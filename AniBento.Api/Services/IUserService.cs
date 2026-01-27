using AniBento.Api.Dtos.User;

namespace AniBento.Api.Services
{
    public interface IUserService
    {
        Task<PublicUserInfoResponse> GetPublicUserInfoByUsernameAsync(
            GetPublicUserInfoRequest request
        );
    }
}
