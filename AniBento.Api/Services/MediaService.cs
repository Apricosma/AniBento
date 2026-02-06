using AniBento.Api.Data;
using AniBento.Api.Dtos.Media;
using AniBento.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AniBento.Api.Services
{
    public class MediaService(AppDbContext context) : IMediaService
    {
        public async Task<GetMediaResponse> CreateMediaAsync(CreateMediaBaseRequest mediaRequest)
        {
            Media media = new Media
            {
                Title = mediaRequest.Title,
                Description = mediaRequest.Description,
                MediaType = mediaRequest.MediaType,
                ReleaseDate = mediaRequest.ReleaseDate,
                Studio = mediaRequest.Studio,
                MediaImageUrl = mediaRequest.MediaImageUrl,
                EpisodeOrChapterCount = mediaRequest.EpisodeOrChapterCount,
            };
            context.Medias.Add(media);
            await context.SaveChangesAsync();

            return new GetMediaResponse
            {
                Id = media.Id,
                Title = media.Title,
                Description = media.Description,
                MediaType = media.MediaType,
                ReleaseDate = media.ReleaseDate,
                Studio = media.Studio,
                Publisher = media.Publisher,
                MediaImageUrl = media.MediaImageUrl,
                EpisodeOrChapterCount = media.EpisodeOrChapterCount,
                EnteredAt = media.enteredAt,
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

        public async Task<List<GetMediaResponse>> GetAllMediaAsync()
        {
            return await context
                .Medias.Select(m => new GetMediaResponse
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    MediaType = m.MediaType,
                    ReleaseDate = m.ReleaseDate,
                    Studio = m.Studio,
                    MediaImageUrl = m.MediaImageUrl,
                    EpisodeOrChapterCount = m.EpisodeOrChapterCount,
                    EnteredAt = m.enteredAt,
                })
                .OrderBy(m => m.Id)
                .ToListAsync();
        }

        public async Task<GetMediaResponse?> GetMediaByIdAsync(int id)
        {
            GetMediaResponse? media = await context
                .Medias.Where(context => context.Id == id)
                .Select(m => new GetMediaResponse
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    MediaType = m.MediaType,
                    ReleaseDate = m.ReleaseDate,
                    Studio = m.Studio,
                    MediaImageUrl = m.MediaImageUrl,
                    EpisodeOrChapterCount = m.EpisodeOrChapterCount,
                    EnteredAt = m.enteredAt,
                })
                .FirstOrDefaultAsync();
            return media;
        }

        public async Task<bool> UpdateMediaAsync(int id, UpdateMediaRequest media)
        {
            Media? existingMedia = await context.Medias.FindAsync(id);
            if (existingMedia is null)
                return false;

            existingMedia.Title = media.Title;
            existingMedia.Description = media.Description;
            existingMedia.MediaType = media.MediaType;
            existingMedia.ReleaseDate = media.ReleaseDate;
            existingMedia.Studio = media.Studio;
            existingMedia.Publisher = media.Publisher;
            existingMedia.MediaImageUrl = media.MediaImageUrl;
            existingMedia.EpisodeOrChapterCount = media.EpisodeOrChapterCount;

            await context.SaveChangesAsync();
            return true;
        }
    }
}
