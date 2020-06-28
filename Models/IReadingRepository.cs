using System.Collections.Generic;

namespace BookClubApi.Models
{
    public interface IReadingRepository
    {
        Reading AddReading(Reading reading);
        IEnumerable<Reading> GetReadingsByUserId(int userId);
    }
}