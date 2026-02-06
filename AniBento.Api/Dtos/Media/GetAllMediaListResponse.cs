using AniBento.Api.Models.Enums;

namespace AniBento.Api.Dtos.Media
{
    public class GetAllMediaListResponse
    {
        public int Id { get; set; }

        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string? MediaImageUrl { get; set; }
        public DateTimeOffset EnteredAt { get; set; }

        public MediaType MediaType { get; set; }
    }
}
