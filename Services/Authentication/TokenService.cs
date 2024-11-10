using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api.Models.User;
using Microsoft.IdentityModel.Tokens;

namespace api.Services.Authentication
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        public string Generate(AppUserModel user, string role)
        {
            var key = System.Text.Encoding.UTF8.GetBytes(configuration["JWT:SignInKey"] ?? "");

            SecurityTokenDescriptor tokenDescriptor =
                new()
                {
                    Subject = new ClaimsIdentity(
                        [
                            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                            new Claim(ClaimTypes.Role, role),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        ]
                    ),
                    Expires = DateTime.UtcNow.AddDays(5),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha384Signature
                    ),
                };
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
