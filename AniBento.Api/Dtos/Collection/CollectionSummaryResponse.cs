namespace AniBento.Api.Dtos.Collection
{
    public class CollectionSummaryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsPrivate { get; set; }
        public int ItemCount { get; set; }
        public required bool IsPinned { get; set; }
    }
}
