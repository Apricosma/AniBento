using AniBento.Api.Models.Enums;

namespace AniBento.Api.Dtos.UserMedia
{
    public class UserMediaResponse
    {
        public int MediaId { get; set; }
        public string Title { get; set; } = string.Empty;

        public UserMediaStatus Status { get; set; }
        public int? Rating { get; set; }

        public DateTimeOffset AddedAt { get; set; }
    }
}
