using AniBento.Api.Dtos.Common;
using AniBento.Api.Dtos.Media;
using AniBento.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AniBento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController(IMediaService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResponse<MediaListItem>>> GetMedia(
            [FromQuery] GetAllMediaQuery query,
            CancellationToken ct
        )
        {
            PagedResponse<MediaListItem> result = await service.GetAllPagedAsync(query, ct);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetMediaResponse>> GetMediaById(int id, CancellationToken ct)
        {
            GetMediaResponse? media = await service.GetMediaByIdAsync(id, ct);
            if (media is null)
                return NotFound();
            return Ok(media);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMediaById(int id, CancellationToken ct)
        {
            await service.DeleteMediaAsync(id, ct);
            return NoContent();
        }

        [Authorize]
        [HttpPost("anime")]
        [SwaggerOperation(Summary = "Create an Anime media item.")]
        public async Task<ActionResult<GetMediaResponse>> CreateAnime(
            [FromBody] CreateAnimeRequest req,
            CancellationToken ct
        )
        {
            var created = await service.CreateAnimeAsync(req, ct);
            return CreatedAtAction(nameof(GetMediaById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPost("manga")]
        [SwaggerOperation(Summary = "Create a Manga media item.")]
        public async Task<ActionResult<GetMediaResponse>> CreateManga(
            [FromBody] CreateMangaRequest req,
            CancellationToken ct
        )
        {
            var created = await service.CreateMangaAsync(req, ct);
            return CreatedAtAction(nameof(GetMediaById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPost("movie")]
        [SwaggerOperation(Summary = "Create a Movie media item.")]
        public async Task<ActionResult<GetMediaResponse>> CreateMovie(
            [FromBody] CreateMovieRequest req,
            CancellationToken ct
        )
        {
            var created = await service.CreateMovieAsync(req, ct);
            return CreatedAtAction(nameof(GetMediaById), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPut("anime/{id:int}")]
        [SwaggerOperation(Summary = "Update an existing Anime media item by ID.")]
        public async Task<ActionResult<GetMediaResponse>> UpdateAnime(
            int id,
            [FromBody] UpdateAnimeRequest req,
            CancellationToken ct
        )
        {
            var updated = await service.UpdateAnimeAsync(id, req, ct);
            if (updated is null)
                return NotFound();
            return Ok(updated);
        }

        [Authorize]
        [HttpPut("manga/{id:int}")]
        [SwaggerOperation(Summary = "Update an existing Manga media item by ID.")]
        public async Task<ActionResult<GetMediaResponse>> UpdateManga(
            int id,
            [FromBody] UpdateMangaRequest req,
            CancellationToken ct
        )
        {
            var updated = await service.UpdateMangaAsync(id, req, ct);
            if (updated is null)
                return NotFound();
            return Ok(updated);
        }

        [Authorize]
        [HttpPut("movie/{id:int}")]
        [SwaggerOperation(Summary = "Update an existing Movie media item by ID.")]
        public async Task<ActionResult<GetMediaResponse>> UpdateMovie(
            int id,
            [FromBody] UpdateMovieRequest req,
            CancellationToken ct
        )
        {
            var updated = await service.UpdateMovieAsync(id, req, ct);
            if (updated is null)
                return NotFound();
            return Ok(updated);
        }
    }
}
