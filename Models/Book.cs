namespace BookClubApi.Models
{
    public class Book 
    {
        // define properties of the Book obj
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public int Chapters { get; set; }
        public bool IsCurrentBook { get; set; }
    }
}