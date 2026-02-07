using System.Text.Json.Serialization;
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

        public List<GenreDto> Genres { get; set; } = [];

        // If a field is null, it will be ignored in the JSON response
        // This is to avoid sending unnecessary data for media types that don't have specific details
        // Front-end needs to handle the absence of these fields appropriately
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AnimeDetailsDto? Anime { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MangaDetailsDto? Manga { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MovieDetailsDto? Movie { get; set; }
    }

    public class AnimeDetailsDto
    {
        public string? Studio { get; set; }
        public int EpisodeCount { get; set; }
    }

    public class MangaDetailsDto
    {
        public string? Publisher { get; set; }
        public int ChapterCount { get; set; }
        public int VolumeCount { get; set; }
    }

    public class MovieDetailsDto
    {
        public string? Studio { get; set; }
        public string[]? Directors { get; set; }
    }
}
