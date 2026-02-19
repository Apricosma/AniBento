using AniBento.Api.Dtos.Collection;

namespace AniBento.Api.Services
{
    public interface ICollectionService
    {
        Task<IReadOnlyCollection<CollectionSummaryResponse>> GetMyCollectionsAsync(
            CancellationToken ct
        );
        Task<CollectionSummaryResponse> CreateAsync(
            CreateCollectionRequest request,
            CancellationToken ct
        );
        Task<CollectionSummaryResponse?> GetCollectionSummaryByIdAsync(
            int id,
            CancellationToken ct
        );
        Task<CollectionResponse?> GetCollectionByIdAsync(int id, CancellationToken ct);
        Task<List<CollectionSummaryResponse>> GetCollectionSummariesByUserIdAsync(
            string userId,
            CancellationToken ct
        );

        Task<CollectionSummaryResponse?> UpdateCollectionAsync(
            int collectionId,
            UpdateCollectionRequest request,
            CancellationToken ct
        );

        Task<bool> DeleteCollectionAsync(int collectionId, CancellationToken ct);

        Task<bool> AddItemToCollectionAsync(
            int collectionId,
            AddCollectionItemRequest request,
            CancellationToken ct
        );
        Task RemoveMediaFromCollectionAsync(
            int collectionId,
            int collectionItemId,
            CancellationToken ct
        );
        Task<IReadOnlyList<CollectionSummaryResponse>> GetCollectionsForUserAsync(
            string userName,
            CancellationToken ct
        );

        Task<CollectionResponse?> GetCollectionForUserByIdAsync(
            string userName,
            int collectionId,
            CancellationToken ct
        );

        Task<bool> CollectionItemUpdateAsync(
            int collectionId,
            int collectionItemId,
            UpdateCollectionItemRequest request,
            CancellationToken ct
        );
    }
}
