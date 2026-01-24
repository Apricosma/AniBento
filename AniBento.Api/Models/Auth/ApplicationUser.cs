using Microsoft.AspNetCore.Identity;

namespace AniBento.Api.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<UserMedia> UserMedias { get; set; } = new List<UserMedia>();
    }
}
