namespace AniBento.Api.Dtos.Collection
{
    public class UpdateCollectionRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsPrivate { get; set; }
    }
}
