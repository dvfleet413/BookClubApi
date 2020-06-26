using System.ComponentModel.DataAnnotations;

namespace BookClubApi.DTOs
{
    public class RegisterUserDto 
    {
        [Required]
        public string Username { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}