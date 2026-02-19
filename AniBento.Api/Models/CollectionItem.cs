namespace AniBento.Api.Models
{
    public class CollectionItem
    {
        public int Id { get; set; }

        public int CollectionId { get; set; }
        public MediaCollection Collection { get; set; } = default!;

        public int UserMediaId { get; set; }
        public UserMedia UserMedia { get; set; } = default!;

        public DateTimeOffset AddedAt { get; set; } = DateTimeOffset.UtcNow;

        public string? Note { get; set; }
    }
}
