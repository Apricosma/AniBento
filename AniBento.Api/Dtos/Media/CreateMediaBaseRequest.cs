using AniBento.Api.Models.Enums;

namespace AniBento.Api.Dtos.Media
{
    public class CreateMediaBaseRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? MediaImageUrl { get; set; }
    }

    public class CreateAnimeRequest : CreateMediaBaseRequest
    {
        public string? Studio { get; set; }
        public int EpisodeCount { get; set; }
        public string[] Genres { get; set; } = [];
    }

    public class CreateMangaRequest : CreateMediaBaseRequest
    {
        public string? Publisher { get; set; }
        public int ChapterCount { get; set; }
        public int VolumeCount { get; set; }
        public string[] Genres { get; set; } = [];
    }

    public class CreateMovieRequest : CreateMediaBaseRequest
    {
        public string? Studio { get; set; }
        public string[] Genres { get; set; } = [];
        public string[]? Directors { get; set; }
    }
}
