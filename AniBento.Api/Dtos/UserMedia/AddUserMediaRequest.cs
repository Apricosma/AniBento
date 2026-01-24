using AniBento.Api.Models.Enums;

namespace AniBento.Api.Dtos.UserMedia
{
    public class AddUserMediaRequest
    {
        public int MediaId { get; set; }
        public UserMediaStatus Status { get; set; }
        public int? Rating { get; set; }
    }
}
