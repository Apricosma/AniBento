using AniBento.Api.Models.Auth;

namespace AniBento.Api.Models
{
    public class MediaCollection
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string Description { get; set; } = string.Empty;

        public bool IsPrivate { get; set; }

        public bool IsPinned { get; set; } = false;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public ICollection<CollectionItem> CollectionItems { get; set; } =
            new List<CollectionItem>();
    }
}
