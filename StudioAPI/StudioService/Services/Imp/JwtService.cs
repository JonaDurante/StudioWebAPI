using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudioModel.Domain;
using StudioService.Services;

namespace StudioService.LoginService.Imp
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public UserToken GeneratedToken(UserApp userApp)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration[key:"JWT:Key"]!);                      
            double.TryParse(_configuration[key: "JWT:ExpiredTime"], out var expiredTimeDay);            
            var expiredTime = DateTime.UtcNow.AddDays(expiredTimeDay);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = expiredTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new [] {new Claim("Id", userApp.Id), 
                    new Claim("Role", userApp.Role.ToLower())})
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenHandlerResult = tokenHandler.WriteToken(token);

            return new UserToken()
            {
                Id = Guid.NewGuid(),
                ExpiredTime = expiredTime,
                Rol = userApp.Role,
                Token = tokenHandlerResult,
                UserName = userApp.UserName!,
                Validity = expiredTime - DateTime.UtcNow
            };
        }
        public UserToken RefreshToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Key"]!);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero 
                }, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    var userId = principal.FindFirst("Id")?.Value;
                    var role = principal.FindFirst("Role")?.Value;

                    if (userId != null && role != null)
                    {
                        var userApp = new UserApp
                        {
                            Id = userId,
                            Role = role
                        };
                        return GeneratedToken(userApp); 
                    }
                }
            }
            catch (SecurityTokenException)
            {
                  throw new SecurityTokenException("Token inválido o expirado");
            }

            throw new SecurityTokenException("No se pudo refrescar el token");
        }

    }
}
