using System.Threading.Tasks;
using BookClubApi.DTOs;

namespace BookClubApi.Models
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password); 
        Task<ResponseUserDto> Login(string username, string pasword);
        Task<bool> UserExists(string username);
        Task<User> GetUserByUsername(string username);
    }
}