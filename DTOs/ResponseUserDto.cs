using System.Collections.Generic;
using BookClubApi.Models;

namespace BookClubApi.DTOs
{
    public class ResponseUserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<ResponseBookDto> Books { get; set; }
    }
}