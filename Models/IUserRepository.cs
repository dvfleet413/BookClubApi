using BookClubApi.DTOs;

namespace BookClubApi.Models
{
    public interface IUserRepository
    {
        ResponseUserDto GetUserById(int userId);
        ResponseUserDto ActivateUser(int userId);
        ResponseUserDto DeactivateUser(int userId);
        ResponseUserDto UpdateEmail(int userId, string email);
    }
}