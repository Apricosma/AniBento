namespace AniBento.Api.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MediaType { get; set; } // e.g., "Anime", "Manga"
        public DateTime ReleaseDate { get; set; }
        public string? Studio { get; set; }
        public string? Publisher { get; set; }
        public string? MediaImageUrl { get; set; }
        public int EpisodeOrChapterCount { get; set; }
        public DateTime enteredAt { get; set; }

        public ICollection<UserMedia> UserMedias { get; set; } = new List<UserMedia>();
    }
}
