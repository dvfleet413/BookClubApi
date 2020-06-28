namespace BookClubApi.Models 
{
    public class Reading
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int ChaptersFinished { get; set; }
    }
}