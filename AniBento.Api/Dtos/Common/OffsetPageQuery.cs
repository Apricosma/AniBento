namespace AniBento.Api.Dtos.Common
{
    public class OffsetPageQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}
