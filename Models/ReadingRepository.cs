using System.Collections.Generic;
using System.Linq;

namespace BookClubApi.Models
{
    public class ReadingRepository : IReadingRepository
    {
        private readonly AppDbContext _appDbContext;
        public ReadingRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Reading AddReading(Reading reading)
        {
            _appDbContext.Readings.Add(reading);
            _appDbContext.SaveChanges();
            return reading;
        }

        public IEnumerable<Reading> GetReadingsByUserId(int userId)
        {
            return _appDbContext.Readings.Where(r => r.UserId == userId);
        }
    }
}