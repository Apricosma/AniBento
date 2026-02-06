using AniBento.Api.Dtos.Media;
using AniBento.Api.Models;
using AniBento.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AniBento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController(IMediaService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<GetAllMediaListResponse>>> GetMedia()
        {
            return Ok(await service.GetAllMediaAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetMediaResponse>> GetMediaById(int id)
        {
            GetMediaResponse? media = await service.GetMediaByIdAsync(id);
            if (media == null)
            {
                return NotFound();
            }
            return Ok(media);
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMediaById(int id)
        {
            await service.DeleteMediaAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "User")]
        [HttpPost("anime")]
        [SwaggerOperation(Summary = "Create an Anime media item.")]
        public async Task<ActionResult<GetMediaResponse>> CreateAnime(
            [FromBody] CreateAnimeRequest req
        )
        {
            var created = await service.CreateAnimeAsync(req);
            return CreatedAtAction(nameof(GetMediaById), new { id = created.Id }, created);
        }

        [Authorize(Roles = "User")]
        [HttpPost("manga")]
        [SwaggerOperation(Summary = "Create a Manga media item.")]
        public async Task<ActionResult<GetMediaResponse>> CreateManga(
            [FromBody] CreateMangaRequest req
        )
        {
            var created = await service.CreateMangaAsync(req);
            return CreatedAtAction(nameof(GetMediaById), new { id = created.Id }, created);
        }

        [Authorize(Roles = "User")]
        [HttpPost("movie")]
        [SwaggerOperation(Summary = "Create a Movie media item.")]
        public async Task<ActionResult<GetMediaResponse>> CreateMovie(
            [FromBody] CreateMovieRequest req
        )
        {
            var created = await service.CreateMovieAsync(req);
            return CreatedAtAction(nameof(GetMediaById), new { id = created.Id }, created);
        }

        [Authorize(Roles = "User")]
        [HttpPut("anime/{id:int}")]
        [SwaggerOperation(Summary = "Update an existing Anime media item by ID.")]
        public async Task<ActionResult<GetMediaResponse>> UpdateAnime(
            int id,
            [FromBody] UpdateAnimeRequest req
        )
        {
            var updated = await service.UpdateAnimeAsync(id, req);
            if (updated is null)
                return NotFound();
            return Ok(updated);
        }

        [Authorize(Roles = "User")]
        [HttpPut("manga/{id:int}")]
        [SwaggerOperation(Summary = "Update an existing Manga media item by ID.")]
        public async Task<ActionResult<GetMediaResponse>> UpdateManga(
            int id,
            [FromBody] UpdateMangaRequest req
        )
        {
            var updated = await service.UpdateMangaAsync(id, req);
            if (updated is null)
                return NotFound();
            return Ok(updated);
        }

        [Authorize(Roles = "User")]
        [HttpPut("movie/{id:int}")]
        [SwaggerOperation(Summary = "Update an existing Movie media item by ID.")]
        public async Task<ActionResult<GetMediaResponse>> UpdateMovie(
            int id,
            [FromBody] UpdateMovieRequest req
        )
        {
            var updated = await service.UpdateMovieAsync(id, req);
            if (updated is null)
                return NotFound();
            return Ok(updated);
        }
    }
}
