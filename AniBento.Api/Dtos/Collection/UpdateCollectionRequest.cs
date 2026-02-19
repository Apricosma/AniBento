using System.ComponentModel.DataAnnotations;

namespace AniBento.Api.Dtos.Collection
{
    public class UpdateCollectionRequest
    {
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(1000)]
        public required string Description { get; set; }
        public required bool IsPrivate { get; set; }
    }
}
