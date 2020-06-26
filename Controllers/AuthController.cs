using Microsoft.AspNetCore.Mvc;
using BookClubApi.Models;
using BookClubApi.DTOs;
using System.Threading.Tasks;

namespace BookClubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            registerUserDto.Username = registerUserDto.Username.ToLower();

            if (await _authRepository.UserExists(registerUserDto.Username))
            {
                return BadRequest("Usename is already taken.");
            }

            var newUser = new User {
                Username = registerUserDto.Username
            };

            await _authRepository.Register(newUser, registerUserDto.Password);

            return Ok(newUser);
        }
    }
}