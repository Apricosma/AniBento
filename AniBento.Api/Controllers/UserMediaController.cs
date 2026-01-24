using AniBento.Api.Dtos.UserMedia;
using AniBento.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AniBento.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserMediaController(IUserMediaService service) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> AddMediaToCurrentUser(
            [FromBody] AddUserMediaRequest request
        )
        {
            var result = await service.AddMediaToCurrentUserAsync(request);
            return Ok(result);
        }

        [HttpGet("my-library")]
        public async Task<IActionResult> GetUserMedia()
        {
            return Ok(await service.GetCurrentUserMediaAsync());
        }
    }
}
