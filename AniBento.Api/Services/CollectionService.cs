using System.Runtime.CompilerServices;
using AniBento.Api.Data;
using AniBento.Api.Dtos.Collection;
using AniBento.Api.Models;
using AniBento.Api.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AniBento.Api.Services
{
    public class CollectionService(
        AppDbContext context,
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor
    ) : ICollectionService
    {
        private async Task<ApplicationUser?> TryGetCurrentUserAsync()
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext is null)
                return null;
            return await userManager.GetUserAsync(httpContext.User);
        }

        private async Task<ApplicationUser> RequireCurrentUserAsync()
        {
            var user = await TryGetCurrentUserAsync();
            if (user is null)
                throw new UnauthorizedAccessException("User not authenticated.");

            return user;
        }

        public async Task<IReadOnlyCollection<CollectionSummaryResponse>> GetMyCollectionsAsync(
            CancellationToken ct
        )
        {
            var userId = (await RequireCurrentUserAsync()).Id;

            return await context
                .Collections.Where(c => c.UserId == userId)
                .OrderBy(c => c.Name)
                .Select(c => new CollectionSummaryResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsPrivate = c.IsPrivate,
                    ItemCount = c.CollectionItems.Count,
                })
                .ToListAsync(ct);
        }

        public async Task<CollectionSummaryResponse?> GetCollectionSummaryByIdAsync(
            int id,
            CancellationToken ct
        )
        {
            var userId = (await RequireCurrentUserAsync()).Id;

            return await context
                .Collections.Where(c => c.Id == id && c.UserId == userId)
                .Select(c => new CollectionSummaryResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsPrivate = c.IsPrivate,
                    ItemCount = c.CollectionItems.Count,
                })
                .FirstOrDefaultAsync(ct);
        }

        public async Task<CollectionResponse?> GetCollectionByIdAsync(int id, CancellationToken ct)
        {
            var userId = (await RequireCurrentUserAsync()).Id;

            return await context
                .Collections.Where(c => c.Id == id && c.UserId == userId)
                .Select(c => new CollectionResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsPrivate = c.IsPrivate,
                    ItemCount = c.CollectionItems.Count,
                    Items = c
                        .CollectionItems.OrderBy(ci => ci.Id)
                        .Select(ci => new CollectionItemResponse
                        {
                            CollectionItemId = ci.Id,
                            UserMediaId = ci.UserMediaId,
                            MediaId = ci.UserMedia.MediaId,
                            Title = ci.UserMedia.Media.Title,
                            MediaImageUrl = ci.UserMedia.Media.MediaImageUrl,
                            Rating = ci.UserMedia.Rating,
                            Status = ci.UserMedia.Status,
                            Note = ci.Note,
                        })
                        .ToList(),
                })
                .FirstOrDefaultAsync(ct);
        }

        public async Task<IReadOnlyList<CollectionSummaryResponse>?> GetCollectionsForUserAsync(
            string userName,
            CancellationToken ct
        )
        {
            var normalized = userName.ToUpperInvariant();
            var targetUserId = await context
                .Users.Where(u => u.NormalizedUserName == normalized)
                .Select(u => u.Id)
                .SingleOrDefaultAsync(ct);

            if (targetUserId is null)
                return null;

            var currentUser = await TryGetCurrentUserAsync();
            var includePrivate = currentUser is not null && currentUser.Id == targetUserId;

            return await context
                .Collections.Where(c =>
                    c.UserId == targetUserId && (includePrivate || !c.IsPrivate)
                )
                .OrderBy(c => c.Name)
                .Select(c => new CollectionSummaryResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsPrivate = c.IsPrivate,
                    ItemCount = c.CollectionItems.Count,
                })
                .ToListAsync(ct);
        }

        public async Task<CollectionResponse?> GetCollectionForUserByIdAsync(
            string userName,
            int collectionId,
            CancellationToken ct
        )
        {
            var user = await userManager.FindByNameAsync(userName);
            var targetUserId = user?.Id;

            if (targetUserId is null)
                return null;

            var currentUser = await TryGetCurrentUserAsync();
            var includePrivate = currentUser is not null && currentUser.Id == targetUserId;

            return await context
                .Collections.Where(c =>
                    c.Id == collectionId
                    && c.UserId == targetUserId
                    && (includePrivate || !c.IsPrivate)
                )
                .Select(c => new CollectionResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsPrivate = c.IsPrivate,
                    ItemCount = c.CollectionItems.Count,
                    Items = c
                        .CollectionItems.OrderBy(ci => ci.Id)
                        .Select(ci => new CollectionItemResponse
                        {
                            CollectionItemId = ci.Id,
                            UserMediaId = ci.UserMediaId,
                            MediaId = ci.UserMedia.MediaId,
                            Title = ci.UserMedia.Media.Title,
                            MediaImageUrl = ci.UserMedia.Media.MediaImageUrl,
                            Status = ci.UserMedia.Status,
                            Rating = ci.UserMedia.Rating,
                            Note = ci.Note,
                        })
                        .ToList(),
                })
                .SingleOrDefaultAsync(ct);
        }

        public async Task<bool> AddItemToCollectionAsync(
            int collectionId,
            AddCollectionItemRequest request,
            CancellationToken ct
        )
        {
            var currentUser = await RequireCurrentUserAsync();
            var userId = currentUser.Id;

            var collection = await context
                .Collections.Where(c => c.Id == collectionId && c.UserId == userId)
                .FirstOrDefaultAsync(ct);

            if (collection is null)
                throw new InvalidOperationException(
                    "Collection not found or does not belong to the user."
                );

            var userMedia = await context
                .UserMedias.Where(um => um.MediaId == request.MediaId && um.UserId == userId)
                .FirstOrDefaultAsync(ct);

            if (userMedia is null)
                throw new InvalidOperationException(
                    "UserMedia entry not found for the specified media."
                );

            var existingItem = await context.CollectionItems.AnyAsync(
                ci => ci.CollectionId == collectionId && ci.UserMediaId == userMedia.Id,
                ct
            );

            if (existingItem)
            {
                return false;
            }

            context.CollectionItems.Add(
                new CollectionItem
                {
                    CollectionId = collectionId,
                    UserMediaId = userMedia.Id,
                    Note = request.Note,
                    AddedAt = DateTimeOffset.UtcNow,
                }
            );

            await context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<CollectionSummaryResponse> CreateAsync(
            CreateCollectionRequest request,
            CancellationToken ct
        )
        {
            var currentUser = await RequireCurrentUserAsync();
            var currentUserId = currentUser.Id;

            var newCollection = new MediaCollection
            {
                Name = request.Name,
                Description = request.Description,
                IsPrivate = request.IsPrivate,
                UserId = currentUserId,
                CreatedAt = DateTimeOffset.UtcNow,
            };

            context.Collections.Add(newCollection);
            await context.SaveChangesAsync(ct);

            return new CollectionSummaryResponse
            {
                Id = newCollection.Id,
                Name = newCollection.Name,
                Description = newCollection.Description,
                IsPrivate = newCollection.IsPrivate,
                ItemCount = 0,
            };
        }

        public async Task<bool> DeleteCollectionAsync(int collectionId, CancellationToken ct)
        {
            var currentUser = await RequireCurrentUserAsync();
            var currentUserId = currentUser.Id;

            var collection = await context
                .Collections.Where(c => c.Id == collectionId && c.UserId == currentUserId)
                .SingleOrDefaultAsync(ct);

            if (collection is null)
            {
                return false;
            }

            context.Collections.Remove(collection);
            await context.SaveChangesAsync(ct);

            return true;
        }

        public Task<List<CollectionSummaryResponse>> GetCollectionsByUserIdAsync(
            string userId,
            CancellationToken ct
        ) => throw new NotImplementedException();

        public async Task RemoveMediaFromCollectionAsync(
            int collectionId,
            int collectionItemId,
            CancellationToken ct
        )
        {
            var currentUser = await RequireCurrentUserAsync();
            var userId = currentUser.Id;

            bool ownsCollection = await context.Collections.AnyAsync(
                c => c.Id == collectionId && c.UserId == userId,
                ct
            );

            if (!ownsCollection)
            {
                throw new UnauthorizedAccessException(
                    "Collection not found or does not belong to the user."
                );
            }

            var item = await context
                .CollectionItems.Where(ci =>
                    ci.CollectionId == collectionId && ci.Id == collectionItemId
                )
                .SingleOrDefaultAsync(ct);

            if (item is null)
            {
                throw new KeyNotFoundException(
                    "The specified collection item was not found in the collection."
                );
            }

            context.CollectionItems.Remove(item);

            await context.SaveChangesAsync(ct);
        }

        public async Task<CollectionSummaryResponse?> UpdateCollectionAsync(
            int collectionId,
            UpdateCollectionRequest request,
            CancellationToken ct
        )
        {
            var currentUser = await RequireCurrentUserAsync();
            var currentUserId = currentUser.Id;

            var collectionWithCount = await context
                .Collections.Where(c => c.Id == collectionId && c.UserId == currentUserId)
                .Select(c => new { Collection = c, ItemCount = c.CollectionItems.Count })
                .SingleOrDefaultAsync(ct);

            if (collectionWithCount is null)
            {
                return null;
            }

            var collection = collectionWithCount.Collection;

            collection.Name = request.Name;
            collection.Description = request.Description;
            collection.IsPrivate = request.IsPrivate;

            await context.SaveChangesAsync(ct);

            return new CollectionSummaryResponse
            {
                Id = collection.Id,
                Name = collection.Name,
                Description = collection.Description,
                IsPrivate = collection.IsPrivate,
                ItemCount = collectionWithCount.ItemCount,
            };
        }

        public Task<List<CollectionSummaryResponse>> GetCollectionSummariesByUserIdAsync(
            string userId,
            CancellationToken ct
        ) => throw new NotImplementedException();

        public async Task<bool> CollectionItemUpdateAsync(
            int collectionId,
            int collectionItemId,
            UpdateCollectionItemRequest request,
            CancellationToken ct
        )
        {
            var currentUser = await RequireCurrentUserAsync();
            var currentUserId = currentUser.Id;

            var collectionItem = await context
                .CollectionItems.Include(ci => ci.UserMedia)
                .Where(ci =>
                    ci.Id == collectionItemId
                    && ci.CollectionId == collectionId
                    && ci.UserMedia.UserId == currentUserId
                )
                .FirstOrDefaultAsync(ct);

            if (collectionItem is null)
            {
                return false;
            }

            collectionItem.Note = request.Note;

            await context.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> TogglePinnedAsync(int collectionId, CancellationToken ct)
        {
            var currentUser = await RequireCurrentUserAsync();
            var userId = currentUser.Id;

            var collection = await context.Collections.SingleOrDefaultAsync(
                c => c.Id == collectionId && c.UserId == userId,
                ct
            );

            if (collection is null)
                return false;

            collection.IsPinned = !collection.IsPinned;

            await context.SaveChangesAsync(ct);

            return true;
        }
    }
}
