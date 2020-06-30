namespace BookClubApi.DTOs
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}