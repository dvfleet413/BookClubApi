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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reading>()
                .HasKey(t => new { t.BookId, t.UserId });

            modelBuilder.Entity<Reading>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Readings)
                .HasForeignKey(r => r.BookId);

            modelBuilder.Entity<Reading>()
                .HasOne(r => r.User)
                .WithMany(u => u.Readings)
                .HasForeignKey(r => r.UserId);
        }
    }
}