using AniBento.Api.Data;
using AniBento.Api.Dtos.User;
using Microsoft.EntityFrameworkCore;

namespace AniBento.Api.Services
{
    /// <summary>
    /// Service for fetching public-facing user information
    /// </summary>
    public class UserService(AppDbContext context) : IUserService
    {
        public async Task<PublicUserInfoResponse?> GetPublicUserInfoByUsernameAsync(
            GetPublicUserInfoRequest request
        )
        {
            if (string.IsNullOrWhiteSpace(request.UserName))
                return null;

            string normalizedUsername = request.UserName.ToUpperInvariant();

            return await context
                .Users.Where(u => u.NormalizedUserName == normalizedUsername)
                .Select(u => new PublicUserInfoResponse
                {
                    UserName = u.UserName,
                    ProfilePictureUrl = u.ProfilePictureUrl,
                })
                .FirstOrDefaultAsync();
        }
    }
}
