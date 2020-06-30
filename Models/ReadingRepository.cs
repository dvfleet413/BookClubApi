using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public List<Reading> GetReadingsByUserId(int userId)
        {
            return _appDbContext.Readings.Where(r => r.UserId == userId).Include(r => r.Book).Include(r => r.User).ToList();
        }
    }
}