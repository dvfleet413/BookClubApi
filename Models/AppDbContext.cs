using Microsoft.EntityFrameworkCore;

namespace BookClubApi.Models
{
    public class AppDbContext : DbContext
    {
        // give AppDbContext access to options through constructor injection
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // state which entities DbContext will manage
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}