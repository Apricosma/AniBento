using AniBento.Api.Models.Enums;

namespace AniBento.Api.Dtos.Collection
{
    public class CollectionItemResponse
    {
        public int CollectionItemId { get; set; }
        public int UserMediaId { get; set; }
        public int MediaId { get; set; }
        public string Title { get; set; } = null!;
        public string? MediaImageUrl { get; set; } = null!;
        public UserMediaStatus Status { get; set; }
        public int? Rating { get; set; }
        public string? Note { get; set; }
    }
}
