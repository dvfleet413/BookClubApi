using Microsoft.AspNetCore.Mvc;
using BookClubApi.Models;
using BookClubApi.DTOs;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using System.Net;

namespace BookClubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        // POST "/api/auth/register"
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
                return BadRequest("Username is already taken.");
            }

            if(registerUserDto.Password != registerUserDto.PasswordConfirm)
            {
                return BadRequest("Password and password confirmation don't match.");
            }

            var newUser = new User {
                Username = registerUserDto.Username,
                Email = registerUserDto.Email
            };

            var userDto = await _authRepository.Register(newUser, registerUserDto.Password);

            return Ok(userDto);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            var user = _authRepository.Login(loginUserDto.Username.ToLower(), loginUserDto.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            // Generate JWT and return it in response
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Set cookie to store token in order to persist session
            HttpContext.Response.Cookies.Append("Token", tokenString);

            return Ok(user);
        }

        // POST: "/api/auth/getcurrentuser"
        [HttpPost("getcurrentuser")]
        public IActionResult GetCurrentUser()
        {            
            var cookieList = HttpContext.Request.Cookies.ToList();
            foreach (var pair in cookieList)
            {
                if(pair.Key == "Token")
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwt = tokenHandler.ReadJwtToken(pair.Value);
                    var username = jwt.Claims.FirstOrDefault(c => c.Type == "unique_name").Value;
                    var user = _authRepository.GetUserByUsername(username);
                    if(user == null)
                    {
                        return NotFound("Unable to find user with that username");
                    }
                    return Ok(user);
                }
            }
            return NotFound("No session cookie sent in request");
        }

        // POST: "/api/auth/logout"
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("Token");
            return Ok();
        }
    }
}