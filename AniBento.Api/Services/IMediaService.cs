using AniBento.Api.Dtos.Media;
using AniBento.Api.Models;

namespace AniBento.Api.Services
{
    public interface IMediaService
    {
        Task<List<GetAllMediaListResponse>> GetAllMediaAsync();
        Task<GetMediaResponse?> GetMediaByIdAsync(int id);
        Task<GetMediaResponse> CreateAnimeAsync(CreateAnimeRequest req);
        Task<GetMediaResponse> CreateMangaAsync(CreateMangaRequest req);
        Task<GetMediaResponse> CreateMovieAsync(CreateMovieRequest req);

        Task<GetMediaResponse?> UpdateAnimeAsync(int id, UpdateAnimeRequest req);
        Task<GetMediaResponse?> UpdateMangaAsync(int id, UpdateMangaRequest req);
        Task<GetMediaResponse?> UpdateMovieAsync(int id, UpdateMovieRequest req);
        Task DeleteMediaAsync(int id);
    }
}
