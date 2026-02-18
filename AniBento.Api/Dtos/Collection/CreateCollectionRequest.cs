namespace AniBento.Api.Dtos.Collection
{
    public class CreateCollectionRequest
    {
        public string UserId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsPrivate { get; set; }
    }
}
