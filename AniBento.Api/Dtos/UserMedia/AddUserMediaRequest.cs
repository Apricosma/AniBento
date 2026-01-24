namespace AniBento.Api.Dtos.UserMedia
{
    public class AddUserMediaRequest
    {
        public int MediaId { get; set; }
        public string? Status { get; set; }
        public int? Rating { get; set; }
    }
}
