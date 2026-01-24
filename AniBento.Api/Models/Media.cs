using AniBento.Api.Models.Enums;

namespace AniBento.Api.Models.Enums
{
    public enum MediaType
    {
        Anime,
        Manga,
        LightNovel,
        Movie,
        OVA,
        ONA,
        TVShow,
        WebSeries,
        Other,
    }
}

namespace AniBento.Api.Models
{
    public class Media
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
        public DateTime enteredAt { get; set; }

        public ICollection<UserMedia> UserMedias { get; set; } = new List<UserMedia>();
    }
}
