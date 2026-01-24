namespace AniBento.Api.Dtos.Media
{
    public class CreateMediaRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string MediaType { get; set; } // e.g., "Anime", "Manga"
        public DateTime ReleaseDate { get; set; }
        public string? Studio { get; set; }
        public string? Publisher { get; set; }
        public string? MediaImageUrl { get; set; }
        public int EpisodeOrChapterCount { get; set; }
    }
}
