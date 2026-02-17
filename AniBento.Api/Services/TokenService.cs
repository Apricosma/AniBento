//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using AniBento.Api.Dtos.Auth;
//using AniBento.Api.Models.Auth;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;

//namespace AniBento.Api.Services
//{
//    public class TokenService(IConfiguration configuration) : ITokenService
//    {
//        public AuthResponse CreateToken(ApplicationUser user, IList<string> roles)
//        {
//            var jwtSection = configuration.GetSection("Jwt");
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));

//            var claims = new List<Claim>
//            {
//                new(JwtRegisteredClaimNames.Sub, user.Id),
//                new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
//                new(ClaimTypes.NameIdentifier, user.Id),
//            };

//            foreach (var role in roles)
//            {
//                claims.Add(new Claim(ClaimTypes.Role, role));
//            }

//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//            var expires = DateTime.UtcNow.AddHours(2);

//            var token = new JwtSecurityToken(
//                issuer: jwtSection["Issuer"],
//                audience: jwtSection["Audience"],
//                claims: claims,
//                expires: expires,
//                signingCredentials: creds
//            );

//            return new AuthResponse
//            {
//                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
//                ExpiresAt = expires,
//            };
//        }
//    }
//}
