using AniBento.Api.Data;
using AniBento.Api.Dtos.UserMedia;
using AniBento.Api.Models;
using AniBento.Api.Models.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AniBento.Api.Services
{
    public class UserMediaService(
        AppDbContext context,
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor
    ) : IUserMediaService
    {
        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext is null)
                throw new UnauthorizedAccessException("HTTP context is not available.");

            ApplicationUser? user = await userManager.GetUserAsync(httpContext.User);
            if (user is null)
                throw new UnauthorizedAccessException("User not authenticated.");

            return user;
        }

        public async Task<UserMediaResponse> AddMediaToCurrentUserAsync(AddUserMediaRequest request)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            var entity = await context.UserMedias.FindAsync(user.Id, request.MediaId);
            if (entity != null)
            {
                if (request.Status is not null)
                    entity.Status = request.Status;
                if (request.Rating is not null)
                    entity.Rating = request.Rating;

                context.UserMedias.Update(entity);
            }
            else
            {
                entity = new UserMedia
                {
                    UserId = user.Id,
                    MediaId = request.MediaId,
                    Rating = request.Rating,
                    Status = request.Status,
                    AddedAt = DateTime.UtcNow,
                };

                context.UserMedias.Add(entity);
            }

            Media? media = await context.Medias.FindAsync(entity.MediaId);

            await context.SaveChangesAsync();

            return new UserMediaResponse
            {
                MediaId = entity.MediaId,
                Title = media?.Title ?? string.Empty,
                Status = entity.Status,
                Rating = entity.Rating,
                AddedAt = entity.AddedAt,
            };
        }

        public async Task<List<UserMediaResponse>> GetCurrentUserMediaAsync()
        {
            ApplicationUser? user = await GetCurrentUserAsync();

            return await context
                .UserMedias.Where(um => um.UserId == user.Id)
                .Select(um => new UserMediaResponse
                {
                    MediaId = um.MediaId,
                    Title = um.Media.Title,
                    Status = um.Status,
                    Rating = um.Rating,
                    AddedAt = um.AddedAt,
                })
                .ToListAsync();
        }

        public async Task RemoveMediaFromCurrentUserAsync(int mediaId)
        {
            UserMedia? userMedia = await context
                .UserMedias.Where(um => um.MediaId == mediaId)
                .FirstOrDefaultAsync();
            if (userMedia is null)
                return;

            context.UserMedias.Remove(userMedia);
            await context.SaveChangesAsync();
        }

        // TODO: Make this method work as a list of updates instead of one at a time
        public async Task UpdateCurrentUserMediaRatingByIdAsync(int mediaId, int rating)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext is null)
                throw new UnauthorizedAccessException("HTTP context is not available.");
            ApplicationUser? user = userManager.GetUserAsync(httpContext.User).Result;
            if (user is null)
                throw new UnauthorizedAccessException("User not authenticated.");

            UserMedia? userMedia = context
                .UserMedias.Where(um => um.MediaId == mediaId && um.UserId == user.Id)
                .FirstOrDefault();
            if (userMedia is null)
                throw new KeyNotFoundException("UserMedia entry not found.");

            userMedia.Rating = rating;
            context.UserMedias.Update(userMedia);
            await context.SaveChangesAsync();
        }
    }
}
