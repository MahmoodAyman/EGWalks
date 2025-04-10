using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.IdentityModel.Tokens;

namespace EGWalks.API.Repositories {
    public class TokenRepository : ITokenRepository {
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration) {
            _configuration = configuration;
        }
        public string CreateJWTToken(IdentityUser identityUser, List<string> roles) {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, identityUser.Email));
            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
