using System.ComponentModel.DataAnnotations;

namespace AniBento.Api.Dtos.Collection
{
    public class CreateCollectionRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = default!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public bool IsPrivate { get; set; }
    }
}
