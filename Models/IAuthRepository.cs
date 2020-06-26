using System.Threading.Tasks;

namespace BookClubApi.Models
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password); 
        Task<User> Login(string username, string pasword);
        Task<bool> UserExists(string username);
        Task<User> GetUserByUsername(string username);
    }
}