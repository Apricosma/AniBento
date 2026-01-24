using AniBento.Api.Models.Auth;

public enum UserMediaStatus
{
    Planned,
    Watching,
    Reading,
    Completed,
    OnHold,
    Dropped,
}

namespace AniBento.Api.Models
{
    public class UserMedia
    {
        public string UserId { get; set; } = default;
        public ApplicationUser User { get; set; } = default;

        public int MediaId { get; set; }
        public Media Media { get; set; } = default;

        public UserMediaStatus Status { get; set; } = UserMediaStatus.Planned;
        public int? Rating { get; set; }
        public DateTime AddedAt { get; internal set; }
    }
}
