using AniBento.Api.Models.Enums;

namespace AniBento.Api.Dtos.Media
{
    public class GetMediaResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? MediaImageUrl { get; set; }
        public DateTime EnteredAt { get; set; }
    }

    public class AnimeDetailsDto
    {
        public string? Studio { get; set; }
        public int EpisodeCount { get; set; }
        public string[] Genres { get; set; } = [];
    }

    public class MangaDetailsDto
    {
        public string? Publisher { get; set; }
        public int ChapterCount { get; set; }
        public int VolumeCount { get; set; }
        public string[] Genres { get; set; } = [];
    }

    public class MovieDetailsDto
    {
        public string? Studio { get; set; }
        public string[] Genres { get; set; } = [];
        public string[]? Directors { get; set; }
    }
}
