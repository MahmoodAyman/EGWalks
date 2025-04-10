using EGWalks.API.Models.DTO;
using EGWalks.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EGWalks.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> useManager, ITokenRepository tokenRepository) {
            _userManager = useManager;
            _tokenRepository = tokenRepository;
        }

        // POST : /api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDto) {
            var identityUser = new IdentityUser {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded) {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any()) {
                    await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded) {
                        return Ok("User registered successfully, Please Login");
                    }
                }
            }

            return BadRequest("Something went wrong");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDto) {
            var identityUser = await _userManager.FindByEmailAsync(loginRequestDto.Username);
            if (identityUser != null) {
                var checkPassword
                    = await _userManager.CheckPasswordAsync(identityUser, loginRequestDto.Password);
                if (checkPassword) {
                    var userRoles = await _userManager.GetRolesAsync(identityUser);
                    if (userRoles != null) {

                        var jwtToken = _tokenRepository.CreateJWTToken(identityUser, userRoles.ToList());
                        var response = new LoginResponseDTO {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username or Password Incorrect");

        }
    }
}
