using AniBento.Api.Data;
using AniBento.Api.Data.Queries;
using AniBento.Api.Dtos.Common;
using AniBento.Api.Dtos.Media;
using AniBento.Api.Infrastructure.Paging;
using AniBento.Api.Models;
using AniBento.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace AniBento.Api.Services
{
    public static class MediaFactory
    {
        private static void ApplyBase(Media m, DateOnly? releaseDate, string? mediaImageUrl)
        {
            m.Title = m.Title.Trim();
            m.Description = m.Description.Trim();

            m.TitleNormalized = m.Title.ToUpperInvariant();
            m.DescriptionNormalized = m.Description.ToUpperInvariant();

            m.ReleaseDate = releaseDate;
            m.MediaImageUrl = mediaImageUrl;
            m.EnteredAt = DateTimeOffset.UtcNow;
        }

        private static List<MediaGenre> ToJoins(Media media, IEnumerable<int> genreIds)
        {
            return genreIds.Select(id => new MediaGenre { Media = media, GenreId = id }).ToList();
        }

        public static Media CreateAnime(CreateAnimeRequest req, IEnumerable<int> genreIds)
        {
            var media = new Media
            {
                MediaType = MediaType.Anime,
                Title = req.Title,
                Description = req.Description,
            };

            ApplyBase(media, req.ReleaseDate, req.MediaImageUrl);

            media.AnimeDetails = new AnimeDetails
            {
                Studio = req.Studio,
                EpisodeCount = req.EpisodeCount,
            };

            media.MediaGenres = ToJoins(media, genreIds);

            return media;
        }

        public static Media CreateManga(CreateMangaRequest req, IEnumerable<int> genreIds)
        {
            var media = new Media
            {
                MediaType = MediaType.Manga,
                Title = req.Title,
                Description = req.Description,
            };

            ApplyBase(media, req.ReleaseDate, req.MediaImageUrl);

            media.MangaDetails = new MangaDetails
            {
                Publisher = req.Publisher,
                ChapterCount = req.ChapterCount,
                VolumeCount = req.VolumeCount,
            };

            media.MediaGenres = ToJoins(media, genreIds);

            return media;
        }

        public static Media CreateMovie(CreateMovieRequest req, IEnumerable<int> genreIds)
        {
            var media = new Media
            {
                MediaType = MediaType.Movie,
                Title = req.Title,
                Description = req.Description,
            };

            ApplyBase(media, req.ReleaseDate, req.MediaImageUrl);

            media.MovieDetails = new MovieDetails
            {
                Studio = req.Studio,
                Directors = req.Directors,
            };

            media.MediaGenres = ToJoins(media, genreIds);

            return media;
        }
    }

    public class MediaService(AppDbContext context, IGenreService genreService) : IMediaService
    {
        private const int DefaultPageSize = 25;
        private const int MaxPageSize = 100;

        private static List<GenreDto> MapGenres(Media media) =>
            media
                .MediaGenres.Select(mg => mg.Genre)
                .Select(g => new GenreDto { Id = g.Id, Name = g.Name })
                .ToList();

        public async Task<List<GetAllMediaListResponse>> GetAllMediaAsync(CancellationToken ct)
        {
            return await context
                .Medias.AsNoTracking()
                .OrderBy(m => m.Id)
                .Select(m => new GetAllMediaListResponse
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    ReleaseDate = m.ReleaseDate,
                    MediaImageUrl = m.MediaImageUrl,
                    EnteredAt = m.EnteredAt,
                    MediaType = m.MediaType,
                })
                .ToListAsync(ct);
        }

        public async Task<GetMediaResponse?> GetMediaByIdAsync(int id, CancellationToken ct)
        {
            Media? media = await context
                .Medias.AsNoTracking()
                .Include(m => m.MediaGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.AnimeDetails)
                .Include(m => m.MangaDetails)
                .Include(m => m.MovieDetails)
                .FirstOrDefaultAsync(m => m.Id == id, ct);

            if (media is null)
                return null;

            GetMediaResponse response = new GetMediaResponse
            {
                Id = media.Id,
                MediaType = media.MediaType,
                Title = media.Title,
                Description = media.Description,
                ReleaseDate = media.ReleaseDate,
                MediaImageUrl = media.MediaImageUrl,
                EnteredAt = media.EnteredAt,
                Genres = MapGenres(media),
            };

            switch (media.MediaType)
            {
                case MediaType.Anime when media.AnimeDetails is not null:
                    response.Anime = new AnimeDetailsDto
                    {
                        Studio = media.AnimeDetails.Studio,
                        EpisodeCount = media.AnimeDetails.EpisodeCount,
                    };
                    break;

                case MediaType.Manga when media.MangaDetails is not null:
                    response.Manga = new MangaDetailsDto
                    {
                        Publisher = media.MangaDetails.Publisher,
                        ChapterCount = media.MangaDetails.ChapterCount,
                        VolumeCount = media.MangaDetails.VolumeCount,
                    };
                    break;

                case MediaType.Movie when media.MovieDetails is not null:
                    response.Movie = new MovieDetailsDto
                    {
                        Studio = media.MovieDetails.Studio,
                        Directors = media.MovieDetails.Directors,
                    };
                    break;
            }

            return response;
        }

        public async Task<PagedResponse<MediaListItem>> GetAllPagedAsync(
            GetAllMediaQuery query,
            CancellationToken ct
        )
        {
            var baseQuery = context.Medias.AsNoTracking().Apply(query);

            var projected = baseQuery.Select(m => new MediaListItem
            {
                Id = m.Id,
                Title = m.Title,
                ReleaseDate = m.ReleaseDate,
                MediaImageUrl = m.MediaImageUrl,
                EnteredAt = m.EnteredAt,
                MediaType = m.MediaType,
                Genres = m
                    .MediaGenres.Select(mg => mg.Genre)
                    .Select(g => new GenreDto { Id = g.Id, Name = g.Name })
                    .ToList(),
            });

            return await projected.ToPagedAsync(
                page: query.Page,
                pageSize: query.PageSize,
                defaultPageSize: DefaultPageSize,
                maxPageSize: MaxPageSize,
                ct: ct
            );
        }

        public async Task<GetMediaResponse> CreateAnimeAsync(
            CreateAnimeRequest req,
            CancellationToken ct
        )
        {
            var genreIds = await genreService.RequireIdsAsync(req.GenreIds, ct);
            Media media = MediaFactory.CreateAnime(req, genreIds);

            context.Medias.Add(media);
            await context.SaveChangesAsync(ct);

            await context
                .Entry(media)
                .Collection(m => m.MediaGenres)
                .Query()
                .Include(mg => mg.Genre)
                .LoadAsync(ct);

            return new GetMediaResponse
            {
                Id = media.Id,
                MediaType = media.MediaType,
                Title = media.Title,
                Description = media.Description,
                ReleaseDate = media.ReleaseDate,
                MediaImageUrl = media.MediaImageUrl,
                EnteredAt = media.EnteredAt,
                Genres = MapGenres(media),
                Anime = new AnimeDetailsDto
                {
                    Studio = media.AnimeDetails?.Studio,
                    EpisodeCount = media.AnimeDetails?.EpisodeCount ?? 0,
                },
            };
        }

        public async Task<GetMediaResponse> CreateMangaAsync(
            CreateMangaRequest req,
            CancellationToken ct
        )
        {
            var genreIds = await genreService.RequireIdsAsync(req.GenreIds, ct);
            Media media = MediaFactory.CreateManga(req, genreIds);

            context.Medias.Add(media);
            await context.SaveChangesAsync(ct);

            await context
                .Entry(media)
                .Collection(m => m.MediaGenres)
                .Query()
                .Include(mg => mg.Genre)
                .LoadAsync(ct);

            return new GetMediaResponse
            {
                Id = media.Id,
                MediaType = media.MediaType,
                Title = media.Title,
                Description = media.Description,
                ReleaseDate = media.ReleaseDate,
                MediaImageUrl = media.MediaImageUrl,
                EnteredAt = media.EnteredAt,
                Genres = MapGenres(media),
                Manga = new MangaDetailsDto
                {
                    ChapterCount = media.MangaDetails?.ChapterCount ?? 0,
                    Publisher = media.MangaDetails?.Publisher,
                    VolumeCount = media.MangaDetails?.VolumeCount ?? 0,
                },
            };
        }

        public async Task<GetMediaResponse> CreateMovieAsync(
            CreateMovieRequest req,
            CancellationToken ct
        )
        {
            var genreIds = await genreService.RequireIdsAsync(req.GenreIds, ct);
            Media media = MediaFactory.CreateMovie(req, genreIds);

            context.Medias.Add(media);
            await context.SaveChangesAsync(ct);

            await context
                .Entry(media)
                .Collection(m => m.MediaGenres)
                .Query()
                .Include(mg => mg.Genre)
                .LoadAsync(ct);

            return new GetMediaResponse
            {
                Id = media.Id,
                MediaType = media.MediaType,
                Title = media.Title,
                Description = media.Description,
                ReleaseDate = media.ReleaseDate,
                MediaImageUrl = media.MediaImageUrl,
                EnteredAt = media.EnteredAt,
                Genres = MapGenres(media),
                Movie = new MovieDetailsDto
                {
                    Studio = media.MovieDetails?.Studio,
                    Directors = media.MovieDetails?.Directors,
                },
            };
        }

        public async Task<GetMediaResponse?> UpdateAnimeAsync(
            int id,
            UpdateAnimeRequest req,
            CancellationToken ct
        )
        {
            Media? existingMedia = await context
                .Medias.Include(m => m.MediaGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.AnimeDetails)
                .FirstOrDefaultAsync(m => m.Id == id, ct);

            if (existingMedia is null)
                return null;
            if (existingMedia.MediaType != MediaType.Anime)
                return null;

            existingMedia.Title = req.Title;
            existingMedia.TitleNormalized = req.Title.Trim().ToUpperInvariant();
            existingMedia.Description = req.Description;
            existingMedia.DescriptionNormalized = req.Description.Trim().ToUpperInvariant();
            existingMedia.ReleaseDate = req.ReleaseDate;
            existingMedia.MediaImageUrl = req.MediaImageUrl;

            if (existingMedia.AnimeDetails is null)
                throw new InvalidOperationException(
                    "AnimeDetails should not be null for an anime media."
                );

            existingMedia.AnimeDetails.Studio = req.Studio;
            existingMedia.AnimeDetails.EpisodeCount = req.EpisodeCount;

            var genreIds = await genreService.RequireIdsAsync(req.GenreIds, ct);
            existingMedia.MediaGenres.Clear();
            foreach (var gid in genreIds)
                existingMedia.MediaGenres.Add(
                    new MediaGenre { MediaId = existingMedia.Id, GenreId = gid }
                );

            await context.SaveChangesAsync(ct);

            await context
                .Entry(existingMedia)
                .Collection(m => m.MediaGenres)
                .Query()
                .Include(mg => mg.Genre)
                .LoadAsync(ct);

            return new GetMediaResponse
            {
                Id = existingMedia.Id,
                MediaType = existingMedia.MediaType,
                Title = existingMedia.Title,
                Description = existingMedia.Description,
                ReleaseDate = existingMedia.ReleaseDate,
                MediaImageUrl = existingMedia.MediaImageUrl,
                EnteredAt = existingMedia.EnteredAt,
                Genres = MapGenres(existingMedia),
                Anime = new AnimeDetailsDto
                {
                    Studio = existingMedia.AnimeDetails.Studio,
                    EpisodeCount = existingMedia.AnimeDetails.EpisodeCount,
                },
            };
        }

        public async Task<GetMediaResponse?> UpdateMangaAsync(
            int id,
            UpdateMangaRequest req,
            CancellationToken ct
        )
        {
            Media? existingMedia = await context
                .Medias.Include(m => m.MediaGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.MangaDetails)
                .FirstOrDefaultAsync(m => m.Id == id, ct);

            if (existingMedia is null)
                return null;
            if (existingMedia.MediaType != MediaType.Manga)
                return null;

            existingMedia.Title = req.Title;
            existingMedia.TitleNormalized = req.Title.Trim().ToUpperInvariant();
            existingMedia.Description = req.Description;
            existingMedia.DescriptionNormalized = req.Description.Trim().ToUpperInvariant();
            existingMedia.ReleaseDate = req.ReleaseDate;
            existingMedia.MediaImageUrl = req.MediaImageUrl;

            if (existingMedia.MangaDetails is null)
                throw new InvalidOperationException("MangaDetails should not be null");

            existingMedia.MangaDetails.Publisher = req.Publisher;
            existingMedia.MangaDetails.ChapterCount = req.ChapterCount;
            existingMedia.MangaDetails.VolumeCount = req.VolumeCount;

            var genreIds = await genreService.RequireIdsAsync(req.GenreIds, ct);
            existingMedia.MediaGenres.Clear();
            foreach (var gid in genreIds)
                existingMedia.MediaGenres.Add(
                    new MediaGenre { MediaId = existingMedia.Id, GenreId = gid }
                );

            await context.SaveChangesAsync(ct);

            await context
                .Entry(existingMedia)
                .Collection(m => m.MediaGenres)
                .Query()
                .Include(mg => mg.Genre)
                .LoadAsync(ct);

            return new GetMediaResponse
            {
                Id = existingMedia.Id,
                MediaType = existingMedia.MediaType,
                Title = existingMedia.Title,
                Description = existingMedia.Description,
                ReleaseDate = existingMedia.ReleaseDate,
                MediaImageUrl = existingMedia.MediaImageUrl,
                EnteredAt = existingMedia.EnteredAt,
                Genres = MapGenres(existingMedia),
                Manga = new MangaDetailsDto
                {
                    Publisher = existingMedia.MangaDetails.Publisher,
                    ChapterCount = existingMedia.MangaDetails.ChapterCount,
                    VolumeCount = existingMedia.MangaDetails.VolumeCount,
                },
            };
        }

        public async Task<GetMediaResponse?> UpdateMovieAsync(
            int id,
            UpdateMovieRequest req,
            CancellationToken ct
        )
        {
            Media? existingMedia = await context
                .Medias.Include(m => m.MediaGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieDetails)
                .FirstOrDefaultAsync(m => m.Id == id, ct);

            if (existingMedia is null)
                return null;
            if (existingMedia.MediaType != MediaType.Movie)
                return null;

            existingMedia.Title = req.Title;
            existingMedia.TitleNormalized = req.Title.Trim().ToUpperInvariant();
            existingMedia.Description = req.Description;
            existingMedia.DescriptionNormalized = req.Description.Trim().ToUpperInvariant();
            existingMedia.ReleaseDate = req.ReleaseDate;
            existingMedia.MediaImageUrl = req.MediaImageUrl;

            if (existingMedia.MovieDetails is null)
                throw new InvalidOperationException("MovieDetails should not be null");

            existingMedia.MovieDetails.Studio = req.Studio;
            existingMedia.MovieDetails.Directors = req.Directors;

            var genreIds = await genreService.RequireIdsAsync(req.GenreIds, ct);
            existingMedia.MediaGenres.Clear();
            foreach (var gid in genreIds)
                existingMedia.MediaGenres.Add(
                    new MediaGenre { MediaId = existingMedia.Id, GenreId = gid }
                );

            await context.SaveChangesAsync(ct);

            await context
                .Entry(existingMedia)
                .Collection(m => m.MediaGenres)
                .Query()
                .Include(mg => mg.Genre)
                .LoadAsync(ct);

            return new GetMediaResponse
            {
                Id = existingMedia.Id,
                MediaType = existingMedia.MediaType,
                Title = existingMedia.Title,
                Description = existingMedia.Description,
                ReleaseDate = existingMedia.ReleaseDate,
                MediaImageUrl = existingMedia.MediaImageUrl,
                EnteredAt = existingMedia.EnteredAt,
                Genres = MapGenres(existingMedia),
                Movie = new MovieDetailsDto
                {
                    Studio = existingMedia.MovieDetails.Studio,
                    Directors = existingMedia.MovieDetails.Directors,
                },
            };
        }

        public async Task DeleteMediaAsync(int id, CancellationToken ct)
        {
            Media? media = await context.Medias.FindAsync(new object[] { id }, ct);
            if (media is null)
                return;

            context.Medias.Remove(media);
            await context.SaveChangesAsync(ct);
        }
    }
}
