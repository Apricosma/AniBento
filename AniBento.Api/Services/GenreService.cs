using AniBento.Api.Data;
using AniBento.Api.Dtos.Media;
using AniBento.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AniBento.Api.Services
{
    public interface IGenreService
    {
        Task<List<GenreDto>> GetAllAsync(CancellationToken ct);
        Task<List<Genre>> RequireByIdsAsync(List<int>? ids, CancellationToken ct);
    }

    public sealed class GenreService(AppDbContext context) : IGenreService
    {
        public Task<List<GenreDto>> GetAllAsync(CancellationToken ct) =>
            context
                .Genres.AsNoTracking()
                .OrderBy(g => g.Name)
                .Select(g => new GenreDto { Id = g.Id, Name = g.Name })
                .ToListAsync(ct);

        public async Task<List<Genre>> RequireByIdsAsync(List<int>? ids, CancellationToken ct)
        {
            ids ??= [];
            var distinct = ids.Distinct().ToArray();
            if (distinct.Length == 0)
                return [];

            var genres = await context.Genres.Where(g => distinct.Contains(g.Id)).ToListAsync(ct);

            if (genres.Count != distinct.Length)
                throw new ArgumentException("One or more GenreIds are invalid.");

            return genres;
        }
    }
}
