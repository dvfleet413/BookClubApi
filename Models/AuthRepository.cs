using System.Threading.Tasks;

namespace BookClubApi.Models
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _appDbContext;
        public AuthRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<User> Login(string username, string pasword)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] hashedPassword, passwordSalt;
            CreatePasswordHash(password, out hashedPassword, out passwordSalt);

            user.HashedPassword = hashedPassword;
            user.PasswordSalt = passwordSalt;

            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] hashedPassword, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key;
                hashedPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public Task<bool> UserExists(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}