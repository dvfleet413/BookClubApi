using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookClubApi.DTOs;
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
        public ResponseUserDto Login(string username, string password)
        {
            var user = _appDbContext.Users.Where(u => u.Username == username).Include(u => u.Readings).ThenInclude(r => r.Book).ToList()[0];
            if (user == null)
            {
                return null;
            }

            if(!VerifyPassword(password, user.HashedPassword, user.PasswordSalt))
            {
                return null;
            }
            var userDto = ToUserDto(user);
            return userDto;
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

        public ResponseUserDto GetUserByUsername(string username)
        {
            var user =  _appDbContext.Users.Where(u => u.Username == username).Include(u => u.Readings).ThenInclude(r => r.Book).ToList()[0];
            var userDto = ToUserDto(user);
            return userDto;
        }

        private ResponseUserDto ToUserDto(User user)
        {
            List<ResponseBookDto> books = new List<ResponseBookDto>();
            if (user.Readings != null)
            {
                foreach(var reading in user.Readings)
                {
                    books.Add(new ResponseBookDto {
                        BookId = reading.Book.BookId,
                        Title = reading.Book.Title,
                        Author = reading.Book.Author,
                        ImageUrl = reading.Book.ImageUrl,
                        Chapters = reading.Book.Chapters,
                        IsCurrentBok = reading.Book.IsCurrentBook
                    });
                }
            }

            var userDto = new ResponseUserDto {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                IsActive = user.IsActive,
                Books = books
            };

            return userDto;
        }
    }

    
}