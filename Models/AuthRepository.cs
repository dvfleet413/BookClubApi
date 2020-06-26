using System.Threading.Tasks;

namespace BookClubApi.Models
{
    public class AuthRepository : IAuthRepository
    {
        public Task<User> Login(string username, string pasword)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> Register(User user, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UserExists(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}