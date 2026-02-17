using AniBento.Api.Dtos.Media;
using AniBento.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AniBento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController(IGenreService genreService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres(CancellationToken ct)
        {
            var genres = await genreService.GetAllAsync(ct);
            return Ok(genres);
        }

        [SwaggerOperation(
            Summary = "Get array of genre name strings based on arbitrary amount of ids passed"
        )]
        [HttpPost("names/by-ids")]
        public async Task<ActionResult<IEnumerable<string>>> GetGenreListByIds(
            [FromBody] List<int> ids,
            CancellationToken ct
        )
        {
            var names = await genreService.RequireNamesByIdsAsync(ids, ct);
            return names;
        }
    }
}
