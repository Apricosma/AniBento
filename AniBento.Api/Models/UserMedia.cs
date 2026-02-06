using AniBento.Api.Models.Auth;
using AniBento.Api.Models.Enums;

namespace AniBento.Api.Models.Enums
{
    public enum UserMediaStatus
    {
        Planned,
        Watching,
        Reading,
        Completed,
        OnHold,
        Dropped,
    }
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
        public DateTimeOffset AddedAt { get; internal set; }
    }
}
