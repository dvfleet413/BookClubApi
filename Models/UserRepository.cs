using System.Collections.Generic;
using System.Linq;
using BookClubApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BookClubApi.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public ResponseUserDto GetUserById(int userId)
        {
            var user = SetUser(userId);
            var userDto = ToUserDto(user);
            return userDto;
        }
        public ResponseUserDto ActivateUser(int userId)
        {
            var user = SetUser(userId);
            user.IsActive = true;
            _appDbContext.SaveChanges();
            var userDto = ToUserDto(user);
            return userDto;
        }

        public ResponseUserDto DeactivateUser(int userId)
        {
            var user = SetUser(userId);
            user.IsActive = false;
            _appDbContext.SaveChanges();
            var userDto = ToUserDto(user);
            return userDto;
        }

        public ResponseUserDto UpdateEmail(int userId, string email)
        {
            var user = SetUser(userId);
            user.Email = email;
            _appDbContext.SaveChanges();
            var userDto = ToUserDto(user);
            return userDto;
        }

        private User SetUser(int userId)
        {
            var user =  _appDbContext.Users.Where(u => u.UserId == userId).Include(u => u.Readings).ThenInclude(r => r.Book).ToList()[0];
            return user;
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