using System.ComponentModel.DataAnnotations;

namespace AniBento.Api.Dtos.UserMedia
{
    public class UpdateUserMediaRatingRequest
    {
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
