using System.ComponentModel.DataAnnotations;

namespace AniBento.Api.Dtos.UserMedia
{
    public class UpdateUserMediaReviewRequest
    {
        [StringLength(5000)]
        public string? Review { get; set; }
    }
}
