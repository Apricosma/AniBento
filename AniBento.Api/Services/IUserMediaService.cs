using AniBento.Api.Dtos.UserMedia;
using AniBento.Api.Models;

namespace AniBento.Api.Services
{
    public interface IUserMediaService
    {
        Task<UserMediaResponse> AddMediaToCurrentUserAsync(AddUserMediaRequest request);
        Task RemoveMediaFromCurrentUserAsync(int mediaId);
        Task<List<UserMediaResponse>> GetCurrentUserMediaAsync();
        Task UpdateCurrentUserMediaRatingByIdAsync(
            int mediaId,
            UpdateUserMediaRatingRequest request
        );
    }
}
