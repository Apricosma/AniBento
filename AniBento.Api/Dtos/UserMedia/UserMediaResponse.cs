namespace AniBento.Api.Dtos.UserMedia
{
    public class UserMediaResponse
    {
        public int MediaId { get; set; }
        public string Title { get; set; } = string.Empty;

        public string? Status { get; set; }
        public int? Rating { get; set; }

        public DateTime AddedAt { get; set; }
    }
}
