using KoncarTaskTriangles.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace KoncarTaskTriangles.Helpers
{
    public class TokenHelper
    {
        private IConfiguration _configuration;

        public TokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GenerateToken(UserModel userModel)
        { 
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = Convert.FromBase64String(Startup.StaticConfig["Jwt:Key"]);
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString()),
            };
            var signingCredentials = new SigningCredentials
            (new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
               issuer: Startup.StaticConfig["JWT:Issuer"],
               audience: Startup.StaticConfig["JWT:Audience"],
               expires: DateTime.Now.AddHours(3),
               claims: claims,
               signingCredentials: signingCredentials
               );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
