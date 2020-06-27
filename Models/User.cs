namespace BookClubApi.Models
{
    public class User 
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public byte[] PasswordSalt {get; set; }
        public byte[] HashedPassword { get; set;}
    }
}