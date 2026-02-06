using AniBento.Api.Models.Enums;

namespace AniBento.Api.Dtos.Media
{
    public class UpdateMediaBaseRequest
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? MediaImageUrl { get; set; }
    }

    public class UpdateAnimeRequest : UpdateMediaBaseRequest
    {
        public string? Studio { get; set; }
        public int EpisodeCount { get; set; }
        public string[] Genres { get; set; } = [];
    }

    public class UpdateMangaRequest : UpdateMediaBaseRequest
    {
        public string? Publisher { get; set; }
        public int ChapterCount { get; set; }
        public int VolumeCount { get; set; }
        public string[] Genres { get; set; } = [];
    }

    public class UpdateMovieRequest : UpdateMediaBaseRequest
    {
        public string? Studio { get; set; }
        public string[] Genres { get; set; } = [];
        public string[]? Directors { get; set; }
    }
}
