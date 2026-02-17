using AniBento.Api.Data;
using AniBento.Api.Dtos.Media;
using AniBento.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AniBento.Api.Services
{
    public interface IGenreService
    {
        Task<List<GenreDto>> GetAllAsync(CancellationToken ct);
        Task<List<string>> RequireNamesByIdsAsync(List<int>? ids, CancellationToken ct);
        Task<int[]> RequireIdsAsync(List<int>? ids, CancellationToken ct);
    }

    public sealed class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message) { }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public sealed class GenreService(AppDbContext context) : IGenreService
    {
        public Task<List<GenreDto>> GetAllAsync(CancellationToken ct) =>
            context
                .Genres.AsNoTracking()
                .OrderBy(g => g.Name)
                .Select(g => new GenreDto { Id = g.Id, Name = g.Name })
                .ToListAsync(ct);

        public async Task<int[]> RequireIdsAsync(List<int>? ids, CancellationToken ct)
        {
            if (ids is null || ids.Count == 0)
                throw new ValidationException("At least one genre ID must be provided.");

            var distinctIds = ids.Distinct().ToArray();

            var foundCount = await context
                .Genres.AsNoTracking()
                .Where(g => distinctIds.Contains(g.Id))
                .Select(g => g.Id)
                .CountAsync(ct);

            if (foundCount != distinctIds.Length)
                throw new ValidationException("One or more genre IDs are invalid.");
            return distinctIds;
        }

        public async Task<List<string>> RequireNamesByIdsAsync(List<int>? ids, CancellationToken ct)
        {
            var distinctIds = await RequireIdsAsync(ids, ct);

            return await context
                .Genres.AsNoTracking()
                .Where(g => distinctIds.Contains(g.Id))
                .Select(g => g.Name)
                .ToListAsync(ct);
        }
    }
}
