using AniBento.Api.Dtos.Media;
using AniBento.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AniBento.Api.Data.Queries
{
    /// <summary>
    /// Provides extension methods for querying media lists.
    /// </summary>
    public static class MediaListQueryExtensions
    {
        /// <summary>
        /// Applies filtering and sorting to an IQueryable&lt;Media&gt; based on the parameters in a GetAllMediaQuery. Supports filtering by media type and searching by title or description, with relevance-based sorting when a search term is provided.
        /// </summary>
        /// <param name="q">The IQueryable&lt;Media&gt; to apply the query to.</param>
        /// <param name="query">The query parameters to apply.</param>
        /// <returns>An IQueryable&lt;Media&gt; with the applied filtering and sorting.</returns>
        public static IQueryable<Media> Apply(this IQueryable<Media> q, GetAllMediaQuery query)
        {
            if (query.MediaType is not null)
            {
                q = q.Where(m => m.MediaType == query.MediaType);
            }

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                string term = query.Search.Trim().ToUpperInvariant();
                string like = $"%{term}%";

                q = q.Where(m =>
                    EF.Functions.Like(m.TitleNormalized, like)
                    || EF.Functions.Like(m.DescriptionNormalized, like)
                );

                q = q.OrderByDescending(m => EF.Functions.Like(m.TitleNormalized, $"{term}%"))
                    .ThenBy(m => m.Title)
                    .ThenBy(m => m.Id);

                return q;
            }

            return q.OrderByDescending(m => m.EnteredAt).ThenByDescending(m => m.Id);
        }
    }
}
