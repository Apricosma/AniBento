using AniBento.Api.Models.Auth;

namespace AniBento.Api.Models
{
    public class UserMedia
    {
        public string UserId { get; set; } = default;
        public ApplicationUser User { get; set; } = default;

        public int MediaId { get; set; }
        public Media Media { get; set; } = default;

        public string Status { get; set; } = default; // e.g., "Watching", "Completed", "On-Hold", "Dropped", "Plan to Watch"
        public int? Rating { get; set; }
        public DateTime AddedAt { get; internal set; }
    }
}
