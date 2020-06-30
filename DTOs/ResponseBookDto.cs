using System.Collections.Generic;
using BookClubApi.Models;

namespace BookClubApi.DTOs
{
    public class ResponseBookDto
    {
        public int BookId { get; set; }
        public string Title{ get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public int Chapters { get; set; }
        public bool IsCurrentBok { get; set; }
    }
}