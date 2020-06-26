using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookClubApi.Models
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _appDbContext;
        public AuthRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return null;
            }

            if(!VerifyPassword(password, user.HashedPassword, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private bool VerifyPassword(string password, byte[] hashedPassword, byte[] salt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(salt)){ 
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++){
                    if(computedHash[i] != hashedPassword[i]) 
                    {
                        return false;
                    }
                }    
            }
            return true;
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

        public async Task<bool> UserExists(string username)
        {
            if (await _appDbContext.Users.AnyAsync(u => u.Username == username))
            {
                return true;
            }
            return false;
        }
    }
}