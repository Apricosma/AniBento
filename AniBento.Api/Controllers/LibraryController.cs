using AniBento.Api.Dtos.UserMedia;
using AniBento.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AniBento.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController(IUserMediaService userMediaService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMine()
        {
            return Ok(await userMediaService.GetCurrentUserMediaAsync());
        }

        [HttpPost]
        public async Task<ActionResult> AddMediaToCurrentUser(
            [FromBody] AddUserMediaRequest request
        )
        {
            var result = await userMediaService.AddMediaToCurrentUserAsync(request);
            return Ok(result);
        }

        [HttpDelete("{mediaId:int}")]
        public async Task<ActionResult> RemoveMediaFromCurrentUser(int mediaId)
        {
            await userMediaService.RemoveMediaFromCurrentUserAsync(mediaId);
            return NoContent();
        }

        [HttpPut("{mediaId:int}/rating")]
        public async Task<ActionResult> UpdateRatingForMedia(
            int mediaId,
            [FromBody] UpdateUserMediaRatingRequest request
        )
        {
            await userMediaService.UpdateCurrentUserMediaRatingByIdAsync(mediaId, request);
            return NoContent();
        }
    }
}
