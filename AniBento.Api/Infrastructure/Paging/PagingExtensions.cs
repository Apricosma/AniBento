using AniBento.Api.Dtos.Common;
using Microsoft.EntityFrameworkCore;

namespace AniBento.Api.Infrastructure.Paging
{
    public static class PagingExtensions
    {
        /// <summary>
        /// Normalizes paging parameters by ensuring page is at least 1, pageSize is between 1 and maxPageSize, and calculating the number of items to skip.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="DefaultPageSize"></param>
        /// <param name="maxPageSize"></param>
        /// <returns>
        /// Page number, page size, and number of items to skip for pagination.
        /// </returns>
        public static (int Page, int PageSize, int Skip) Normalize(
            int page,
            int pageSize,
            int DefaultPageSize,
            int maxPageSize
        )
        {
            int p = page < 1 ? 1 : page;

            int ps =
                pageSize < 1 ? DefaultPageSize
                : pageSize > maxPageSize ? maxPageSize
                : pageSize;

            return (p, ps, (p - 1) * ps);
        }

        /// <summary>
        /// Asynchronously converts an IQueryable to a PagedResponse by applying pagination parameters and executing the query to fetch the total count and the paged items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="defaultPageSize"></param>
        /// <param name="maxPageSize"></param>
        /// <param name="ct"></param>
        /// <returns>
        /// A response containing items for the requested page, along with pagination metadata such as total count and total pages.
        /// </returns>
        public static async Task<PagedResponse<T>> ToPagedAsync<T>(
            this IQueryable<T> query,
            int page,
            int pageSize,
            int defaultPageSize,
            int maxPageSize,
            CancellationToken ct
        )
        {
            var (p, ps, skip) = Normalize(page, pageSize, defaultPageSize, maxPageSize);

            int totalCount = await query.CountAsync(ct);

            var items = await query.Skip(skip).Take(ps).ToListAsync(ct);

            int totalPages = (int)Math.Ceiling(totalCount / (double)ps);

            return new PagedResponse<T>
            {
                Items = items,
                Page = p,
                PageSize = ps,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };
        }
    }
}
