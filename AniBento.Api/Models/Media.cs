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
        public MediaType MediaType { get; set; }
        public required string Title { get; set; }
        public string? TitleNormalized { get; set; }
        public required string Description { get; set; }
        public string? DescriptionNormalized { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string? MediaImageUrl { get; set; }
        public DateTimeOffset EnteredAt { get; set; }

        public AnimeDetails? AnimeDetails { get; set; }
        public MangaDetails? MangaDetails { get; set; }
        public MovieDetails? MovieDetails { get; set; }

        public ICollection<UserMedia> UserMedias { get; set; } = new List<UserMedia>();
    }

    public class AnimeDetails
    {
        public int MediaId { get; set; }
        public Media Media { get; set; } = default!;

        public string? Studio { get; set; }
        public int EpisodeCount { get; set; }
        public string[] Genres { get; set; } = [];
    }

    public class MangaDetails
    {
        public int MediaId { get; set; }
        public Media Media { get; set; } = default!;

        public string? Publisher { get; set; }
        public int ChapterCount { get; set; }
        public int VolumeCount { get; set; }
        public string[] Genres { get; set; } = [];
    }

    public class MovieDetails
    {
        public int MediaId { get; set; }
        public Media Media { get; set; } = default!;
        public string? Studio { get; set; }
        public string[]? Directors { get; set; }
        public string[] Genres { get; set; } = [];
    }
}
