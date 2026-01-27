using AniBento.Api.Dtos.User;
using AniBento.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AniBento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetPublicUserInfo(string username)
        {
            var result = await userService.GetPublicUserInfoByUsernameAsync(
                new GetPublicUserInfoRequest { Username = username }
            );

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
