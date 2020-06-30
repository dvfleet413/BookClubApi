using System.Collections.Generic;

namespace BookClubApi.Models
{
    public interface IReadingRepository
    {
        Reading AddReading(Reading reading);
        List<Reading> GetReadingsByUserId(int userId);
    }
}