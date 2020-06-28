using BookClubApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookClubApi.Controllers
{
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class ReadingsController : Controller
    {
        private IReadingRepository _readingRepository;
        public ReadingsController(IReadingRepository readingRepository)
        {
            _readingRepository = readingRepository;
        }

        // GET: "/api/users/1/readings"
        [HttpGet]
        public IActionResult GetReadings(int userId)
        {
            var readings = _readingRepository.GetReadingsByUserId(userId);
            return Ok(readings);
        }

        // POST: "/api/users/1/readings
        [HttpPost]
        public IActionResult PostReading(Reading reading)
        {
            _readingRepository.AddReading(reading);
            return Ok(reading);
        }
        
    }
}