using AniBento.Api.Dtos.Collection;

namespace AniBento.Api.Services
{
    public interface ICollectionService
    {
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

        Task<CollectionSummaryResponse> UpdateCollectionAsync(
            int collectionId,
            UpdateCollectionRequest request,
            CancellationToken ct
        );

        Task DeleteCollectionAsync(int collectionId, CancellationToken ct);

        Task AddItemToCollectionAsync(int collectionId, int mediaId, CancellationToken ct);
        Task RemoveMediaFromCollectionAsync(int collectionId, int mediaId, CancellationToken ct);
    }
}
