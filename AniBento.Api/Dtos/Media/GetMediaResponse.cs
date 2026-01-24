using AniBento.Api.Models.Enums;

namespace AniBento.Api.Dtos.Media
{
    public class GetMediaResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public MediaType MediaType { get; set; } // e.g., "Anime", "Manga"
        public DateTime ReleaseDate { get; set; }
        public string? Studio { get; set; }
        public string? Publisher { get; set; }
        public string? MediaImageUrl { get; set; }
        public int EpisodeOrChapterCount { get; set; }
        public DateTime EnteredAt { get; set; }
    }
}
