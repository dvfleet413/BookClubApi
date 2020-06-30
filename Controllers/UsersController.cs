using Microsoft.AspNetCore.Mvc;
using BookClubApi.Models;
using BookClubApi.DTOs;

namespace BookClubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            return Ok(user);
        }


        // PUT: "/api/users/{id}"
        // PUT requests to this endoint must have a user in the body with properties UserId, IsActive, and Email
        [HttpPut("{id}")]
        public IActionResult PutUser(UpdateUserDto user)
        {
            var userDto = user.IsActive ? _userRepository.ActivateUser(user.UserId) : _userRepository.DeactivateUser(user.UserId);
            userDto = _userRepository.UpdateEmail(user.UserId, user.Email);
            return Ok(userDto);
        }
    }
}