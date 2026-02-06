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
        public async Task<ActionResult<List<GetMediaResponse>>> GetMedia()
        {
            return Ok(await service.GetAllMediaAsync());
        }

        [HttpGet("{id}")]
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
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMediaById(int id)
        {
            try
            {
                await service.DeleteMediaAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult> CreateMedia(CreateMediaBaseRequest mediaRequest)
        {
            GetMediaResponse created = await service.CreateMediaAsync(mediaRequest);
            return CreatedAtAction(nameof(GetMediaById), new { id = created.Id }, created);
        }

        [Authorize(Roles = "User")]
        [SwaggerOperation(Summary = "Update an existing media item by its ID.")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMedia(int id, UpdateMediaRequest mediaRequest)
        {
            try
            {
                await service.UpdateMediaAsync(id, mediaRequest);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound($"Media with given Id was not found");
            }
        }
    }
}
