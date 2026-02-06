using AniBento.Api.Models.Enums;

namespace AniBento.Api.Dtos.Media
{
    public sealed class MediaListItem
    {
        public int Id { get; init; }
        public MediaType MediaType { get; init; }
        public required string Title { get; init; }
        public DateOnly? ReleaseDate { get; init; }
        public string? MediaImageUrl { get; init; }
        public DateTimeOffset EnteredAt { get; init; }
    }
}
