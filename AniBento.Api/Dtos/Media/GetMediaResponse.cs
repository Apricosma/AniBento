using AniBento.Api.Models.Enums;

namespace AniBento.Api.Dtos.Media
{
    public class GetMediaResponse
    {
        public int Id { get; set; }
        public MediaType MediaType { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string? MediaImageUrl { get; set; }
        public DateTimeOffset EnteredAt { get; set; }

        public AnimeDetailsDto? Anime { get; set; }
        public MangaDetailsDto? Manga { get; set; }
        public MovieDetailsDto? Movie { get; set; }
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
