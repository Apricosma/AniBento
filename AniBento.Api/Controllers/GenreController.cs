using AniBento.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AniBento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController(IGenreService genreService) : ControllerBase
    {
        public async Task<ActionResult<IEnumerable<string>>> GetGenres(CancellationToken ct)
        {
            var genres = await genreService.GetAllAsync(ct);
            return Ok(genres);
        }
    }
}
