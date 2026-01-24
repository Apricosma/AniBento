using System.Security.Cryptography.X509Certificates;
using AniBento.Api.Dtos.Auth;
using AniBento.Api.Models.Auth;
using AniBento.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AniBento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService
    ) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
        {
            var user = new ApplicationUser { UserName = request.UserName, Email = request.Email };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await userManager.AddToRoleAsync(user, "User");

            var roles = await userManager.GetRolesAsync(user);
            var token = tokenService.CreateToken(user, roles);

            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            var result = await signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                false
            );
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid email or password.");
            }

            var roles = await userManager.GetRolesAsync(user);
            var token = tokenService.CreateToken(user, roles);

            return Ok(token);
        }

        [HttpPost("check-admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult CheckAdmin()
        {
            return Ok("You are an admin.");
        }
    }
}
