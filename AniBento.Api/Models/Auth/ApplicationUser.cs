using Microsoft.AspNetCore.Identity;

namespace AniBento.Api.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<UserMedia> UserMedias { get; set; } = new List<UserMedia>();
        public ICollection<MediaCollection> Collections { get; set; } = new List<MediaCollection>();
        public string? ProfilePictureUrl { get; set; } = string.Empty;
    }
}
