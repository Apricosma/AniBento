using AniBento.Api.Models.Enums;

namespace AniBento.Api.Dtos.Media
{
    public class GetAllMediaListResponse
    {
        public int Id { get; set; }

        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? MediaImageUrl { get; set; }
        public DateTime EnteredAt { get; set; }

        public MediaType MediaType { get; set; }
    }
}
