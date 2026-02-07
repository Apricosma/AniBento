using AniBento.Api.Dtos.Common;
using AniBento.Api.Models.Enums;

namespace AniBento.Api.Dtos.Media
{
    public class GetAllMediaQuery : OffsetPageQuery
    {
        public MediaType? MediaType { get; init; }
        public string? Search { get; init; }
        public List<int>? GenreIds { get; init; }
    }
}
