using AniBento.Api.Dtos.Collection;

namespace AniBento.Api.Services
{
    public interface ICollectionService
    {
        Task<CollectionSummaryResponse> CreateAsync(
            CreateCollectionRequest request,
            CancellationToken ct
        );
        Task<CollectionSummaryResponse> GetCollectionByIdAsync(int id, CancellationToken ct);
        Task<List<CollectionSummaryResponse>> GetCollectionsByUserIdAsync(
            string userId,
            CancellationToken ct
        );
    }
}
