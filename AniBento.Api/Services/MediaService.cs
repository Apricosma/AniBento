using AniBento.Api.Data;
using AniBento.Api.Dtos.Media;
using AniBento.Api.Models;
using AniBento.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace AniBento.Api.Services
{
    public class MediaService(AppDbContext context) : IMediaService
    {
        public async Task<GetMediaResponse> CreateAnimeAsync(CreateAnimeRequest req)
        {
            Media media = new Media
            {
                MediaType = MediaType.Anime,
                Title = req.Title,
                Description = req.Description,
                ReleaseDate = req.ReleaseDate,
                MediaImageUrl = req.MediaImageUrl,
                enteredAt = DateTime.UtcNow,
                AnimeDetails = new AnimeDetails
                {
                    Studio = req.Studio,
                    EpisodeCount = req.EpisodeCount,
                    Genres = req.Genres,
                },
            };

            context.Medias.Add(media);
            await context.SaveChangesAsync();

            return new GetMediaResponse
            {
                Id = media.Id,
                MediaType = media.MediaType,
                Title = media.Title,
                Description = media.Description,
                ReleaseDate = media.ReleaseDate,
                MediaImageUrl = media.MediaImageUrl,
                EnteredAt = media.enteredAt,

                Anime = new AnimeDetailsDto
                {
                    Studio = media.AnimeDetails?.Studio,
                    EpisodeCount = media.AnimeDetails?.EpisodeCount ?? 0,
                    Genres = media.AnimeDetails?.Genres ?? Array.Empty<string>(),
                },
            };
        }

        public async Task<GetMediaResponse> CreateMangaAsync(CreateMangaRequest req)
        {
            Media media = new Media
            {
                MediaType = MediaType.Manga,
                Title = req.Title,
                Description = req.Description,
                ReleaseDate = req.ReleaseDate,
                MediaImageUrl = req.MediaImageUrl,
                enteredAt = DateTime.UtcNow,
                MangaDetails = new MangaDetails
                {
                    ChapterCount = req.ChapterCount,
                    Publisher = req.Publisher,
                    VolumeCount = req.VolumeCount,
                    Genres = req.Genres,
                },
            };

            context.Medias.Add(media);
            await context.SaveChangesAsync();

            return new GetMediaResponse
            {
                Id = media.Id,
                MediaType = media.MediaType,
                Title = media.Title,
                Description = media.Description,
                ReleaseDate = media.ReleaseDate,
                MediaImageUrl = media.MediaImageUrl,
                EnteredAt = media.enteredAt,

                Manga = new MangaDetailsDto
                {
                    ChapterCount = media.MangaDetails?.ChapterCount ?? 0,
                    Publisher = media.MangaDetails?.Publisher,
                    VolumeCount = media.MangaDetails?.VolumeCount ?? 0,
                    Genres = media.MangaDetails?.Genres ?? Array.Empty<string>(),
                },
            };
        }

        public async Task<GetMediaResponse> CreateMovieAsync(CreateMovieRequest req)
        {
            Media media = new Media
            {
                MediaType = MediaType.Movie,
                Title = req.Title,
                Description = req.Description,
                ReleaseDate = req.ReleaseDate,
                MediaImageUrl = req.MediaImageUrl,
                enteredAt = DateTime.UtcNow,
                MovieDetails = new MovieDetails
                {
                    Directors = req.Directors,
                    Studio = req.Studio,
                    Genres = req.Genres,
                },
            };

            context.Medias.Add(media);
            await context.SaveChangesAsync();

            return new GetMediaResponse
            {
                Id = media.Id,
                MediaType = media.MediaType,
                Title = media.Title,
                Description = media.Description,
                ReleaseDate = media.ReleaseDate,
                MediaImageUrl = media.MediaImageUrl,
                EnteredAt = media.enteredAt,

                Movie = new MovieDetailsDto
                {
                    Studio = media.MovieDetails?.Studio,
                    Directors = media.MovieDetails?.Directors,
                    Genres = media.MovieDetails?.Genres ?? [],
                },
            };
        }

        public async Task<GetMediaResponse?> UpdateAnimeAsync(int id, UpdateAnimeRequest req)
        {
            Media? existingMedia = await context
                .Medias.Include(m => m.AnimeDetails)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMedia is null)
                return null;
            if (existingMedia.MediaType != MediaType.Anime)
                return null;

            existingMedia.Title = req.Title;
            existingMedia.Description = req.Description;
            existingMedia.ReleaseDate = req.ReleaseDate;
            existingMedia.MediaImageUrl = req.MediaImageUrl;

            // an anime should never NOT have details, so if one doesn't, there's something seriously wrong...
            if (existingMedia.AnimeDetails is null)
                throw new InvalidOperationException(
                    "AnimeDetails should not be null for an anime media."
                );

            existingMedia.AnimeDetails.Studio = req.Studio;
            existingMedia.AnimeDetails.EpisodeCount = req.EpisodeCount;
            existingMedia.AnimeDetails.Genres = req.Genres;

            await context.SaveChangesAsync();
            return new GetMediaResponse
            {
                Id = existingMedia.Id,
                MediaType = existingMedia.MediaType,
                Title = existingMedia.Title,
                Description = existingMedia.Description,
                ReleaseDate = existingMedia.ReleaseDate,
                MediaImageUrl = existingMedia.MediaImageUrl,
                EnteredAt = existingMedia.enteredAt,
                Anime = new AnimeDetailsDto
                {
                    Studio = existingMedia.AnimeDetails.Studio,
                    EpisodeCount = existingMedia.AnimeDetails.EpisodeCount,
                    Genres = existingMedia.AnimeDetails.Genres,
                },
            };
        }

        public async Task<GetMediaResponse?> UpdateMangaAsync(int id, UpdateMangaRequest req)
        {
            Media? existingMedia = await context
                .Medias.Include(m => m.MangaDetails)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMedia is null)
                return null;
            if (existingMedia.MediaType != MediaType.Manga)
                return null;

            existingMedia.Title = req.Title;
            existingMedia.Description = req.Description;
            existingMedia.ReleaseDate = req.ReleaseDate;
            existingMedia.MediaImageUrl = req.MediaImageUrl;

            // Ensure MangaDetails is not null before accessing its properties
            if (existingMedia.MangaDetails is null)
                throw new InvalidOperationException("MangaDetails should not be null");

            existingMedia.MangaDetails.Publisher = req.Publisher;
            existingMedia.MangaDetails.ChapterCount = req.ChapterCount;
            existingMedia.MangaDetails.VolumeCount = req.VolumeCount;
            existingMedia.MangaDetails.Genres = req.Genres;

            await context.SaveChangesAsync();
            return new GetMediaResponse
            {
                Id = existingMedia.Id,
                MediaType = existingMedia.MediaType,
                Title = existingMedia.Title,
                Description = existingMedia.Description,
                ReleaseDate = existingMedia.ReleaseDate,
                MediaImageUrl = existingMedia.MediaImageUrl,
                EnteredAt = existingMedia.enteredAt,
                Manga = new MangaDetailsDto
                {
                    ChapterCount = existingMedia.MangaDetails.ChapterCount,
                    Publisher = existingMedia.MangaDetails.Publisher,
                    VolumeCount = existingMedia.MangaDetails.VolumeCount,
                },
            };
        }

        public async Task<GetMediaResponse?> UpdateMovieAsync(int id, UpdateMovieRequest req)
        {
            Media? existingMedia = await context
                .Medias.Include(m => m.MovieDetails)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (existingMedia is null)
                return null;
            if (existingMedia.MediaType != MediaType.Movie)
                return null;

            existingMedia.Title = req.Title;
            existingMedia.Description = req.Description;
            existingMedia.ReleaseDate = req.ReleaseDate;
            existingMedia.MediaImageUrl = req.MediaImageUrl;

            if (existingMedia.MovieDetails is null)
                throw new InvalidOperationException("MovieDetails should not be null");

            existingMedia.MovieDetails.Studio = req.Studio;
            existingMedia.MovieDetails.Directors = req.Directors;
            existingMedia.MovieDetails.Genres = req.Genres;

            await context.SaveChangesAsync();
            return new GetMediaResponse
            {
                Id = id,
                MediaType = existingMedia.MediaType,
                Title = existingMedia.Title,
                Description = existingMedia.Description,
                ReleaseDate = existingMedia.ReleaseDate,
                MediaImageUrl = existingMedia.MediaImageUrl,
                EnteredAt = existingMedia.enteredAt,
                Movie = new MovieDetailsDto
                {
                    Genres = existingMedia.MovieDetails.Genres,
                    Studio = existingMedia.MovieDetails.Studio,
                    Directors = existingMedia.MovieDetails.Directors,
                },
            };
        }

        public async Task DeleteMediaAsync(int id)
        {
            Media? media = await context.Medias.FindAsync(id);
            if (media is null)
                return;

            context.Medias.Remove(media);
            await context.SaveChangesAsync();
        }

        public async Task<List<GetAllMediaListResponse>> GetAllMediaAsync()
        {
            return await context
                .Medias.OrderBy(m => m.Id)
                .Select(m => new GetAllMediaListResponse
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    ReleaseDate = m.ReleaseDate,
                    MediaImageUrl = m.MediaImageUrl,
                    EnteredAt = m.enteredAt,
                    MediaType = m.MediaType,
                })
                .ToListAsync();
        }

        public async Task<GetMediaResponse?> GetMediaByIdAsync(int id)
        {
            Media? media = await context
                .Medias.Include(m => m.AnimeDetails)
                .Include(m => m.MangaDetails)
                .Include(m => m.MovieDetails)
                .FirstOrDefaultAsync(m => m.Id == id);

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
                EnteredAt = media.enteredAt,
            };

            // Depending on the media type, populate the appropriate details in the response
            switch (media.MediaType)
            {
                case MediaType.Anime when media.AnimeDetails is not null:
                    response.Anime = new AnimeDetailsDto
                    {
                        Studio = media.AnimeDetails.Studio,
                        EpisodeCount = media.AnimeDetails.EpisodeCount,
                        Genres = media.AnimeDetails.Genres,
                    };
                    break;
                case MediaType.Manga when media.MangaDetails is not null:
                    response.Manga = new MangaDetailsDto
                    {
                        Publisher = media.MangaDetails.Publisher,
                        ChapterCount = media.MangaDetails.ChapterCount,
                        VolumeCount = media.MangaDetails.VolumeCount,
                        Genres = media.MangaDetails.Genres,
                    };
                    break;
                case MediaType.Movie when media.MovieDetails is not null:
                    response.Movie = new MovieDetailsDto
                    {
                        Studio = media.MovieDetails.Studio,
                        Directors = media.MovieDetails.Directors,
                        Genres = media.MovieDetails.Genres,
                    };
                    break;
            }

            return response;
        }
    }
}
