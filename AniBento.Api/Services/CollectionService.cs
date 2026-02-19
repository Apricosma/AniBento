using AniBento.Api.Data;
using AniBento.Api.Dtos.Collection;
using AniBento.Api.Models;
using AniBento.Api.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AniBento.Api.Services
{
    public class CollectionService(
        AppDbContext context,
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor
    ) : ICollectionService
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

        public async Task AddItemToCollectionAsync(
            int collectionId,
            int mediaId,
            CancellationToken ct
        )
        {
            throw new NotImplementedException();
        }

        public async Task<CollectionSummaryResponse> CreateAsync(
            CreateCollectionRequest request,
            CancellationToken ct
        )
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCollectionAsync(int collectionId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<CollectionSummaryResponse?> GetCollectionSummaryByIdAsync(
            int id,
            CancellationToken ct
        )
        {
            var userId = GetCurrentUserAsync().Result.Id;

            var collection = await context
                .Collections.Where(c => c.Id == id && c.UserId == userId)
                .Select(c => new CollectionSummaryResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsPrivate = c.IsPrivate,
                })
                .FirstOrDefaultAsync();

            if (collection == null)
            {
                return null;
            }

            return collection;
        }

        public async Task<CollectionResponse?> GetCollectionByIdAsync(int id, CancellationToken ct)
        {
            var userId = GetCurrentUserAsync().Result.Id;

            var collection = await context
                .Collections.Where(c => c.Id == id && c.UserId == userId)
                .Select(c => new CollectionResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsPrivate = c.IsPrivate,
                    ItemCount = c.CollectionItems.Count,

                    Items = c.CollectionItems.Select(ci => new CollectionItemResponse
                    {
                        UserMediaId = ci.UserMediaId,
                        MediaId = ci.UserMedia.MediaId,
                        Title = ci.UserMedia.Media.Title,
                        MediaImageUrl = ci.UserMedia.Media.MediaImageUrl,
                        Rating = ci.UserMedia.Rating,
                        Status = ci.UserMedia.Status,
                    }),
                })
                .FirstOrDefaultAsync(cancellationToken: ct);

            if (collection == null)
            {
                return null;
            }

            return collection;
        }

        public async Task<List<CollectionSummaryResponse>> GetCollectionsByUserIdAsync(
            string userId,
            CancellationToken ct
        )
        {
            throw new NotImplementedException();
        }

        public async Task RemoveMediaFromCollectionAsync(
            int collectionId,
            int mediaId,
            CancellationToken ct
        )
        {
            throw new NotImplementedException();
        }

        public async Task<CollectionSummaryResponse> UpdateCollectionAsync(
            int collectionId,
            UpdateCollectionRequest request,
            CancellationToken ct
        )
        {
            throw new NotImplementedException();
        }

        public Task<List<CollectionSummaryResponse>> GetCollectionSummariesByUserIdAsync(
            string userId,
            CancellationToken ct
        )
        {
            throw new NotImplementedException();
        }
    }
}
