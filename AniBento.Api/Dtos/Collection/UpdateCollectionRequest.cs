namespace AniBento.Api.Dtos.Collection
{
    public class UpdateCollectionRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool IsPrivate { get; set; }
    }
}
