using System.Threading.Tasks;
using BookClubApi.DTOs;

namespace BookClubApi.Models
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password); 
        ResponseUserDto Login(string username, string pasword);
        Task<bool> UserExists(string username);
        ResponseUserDto GetUserByUsername(string username);
    }
}