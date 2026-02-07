using AniBento.Api.Dtos.Common;
using AniBento.Api.Dtos.Media;

namespace AniBento.Api.Services
{
    public interface IMediaService
    {
        Task<List<GetAllMediaListResponse>> GetAllMediaAsync(CancellationToken ct);

        Task<GetMediaResponse?> GetMediaByIdAsync(int id, CancellationToken ct);

        Task<PagedResponse<MediaListItem>> GetAllPagedAsync(
            GetAllMediaQuery query,
            CancellationToken ct
        );

        Task<GetMediaResponse> CreateAnimeAsync(CreateAnimeRequest req, CancellationToken ct);

        Task<GetMediaResponse> CreateMangaAsync(CreateMangaRequest req, CancellationToken ct);

        Task<GetMediaResponse> CreateMovieAsync(CreateMovieRequest req, CancellationToken ct);

        Task<GetMediaResponse?> UpdateAnimeAsync(
            int id,
            UpdateAnimeRequest req,
            CancellationToken ct
        );

        Task<GetMediaResponse?> UpdateMangaAsync(
            int id,
            UpdateMangaRequest req,
            CancellationToken ct
        );

        Task<GetMediaResponse?> UpdateMovieAsync(
            int id,
            UpdateMovieRequest req,
            CancellationToken ct
        );

        Task DeleteMediaAsync(int id, CancellationToken ct);
    }
}
