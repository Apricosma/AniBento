using AniBento.Api.Dtos.Media;
using AniBento.Api.Models;

namespace AniBento.Api.Services
{
    public interface IMediaService
    {
        Task<List<GetMediaResponse>> GetAllMediaAsync();
        Task<GetMediaResponse?> GetMediaByIdAsync(int id);
        Task<GetMediaResponse> CreateMediaAsync(CreateMediaRequest media);
        Task<bool> UpdateMediaAsync(int id, UpdateMediaRequest media);
        Task DeleteMediaAsync(int id);
    }
}
