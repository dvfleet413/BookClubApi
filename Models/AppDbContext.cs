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
        public DbSet<Reading> Readings { get; set; }

        // this override method is required to establish many to many relationship. feature may be added to EF Core soon to do this by default
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reading>()
                .HasKey(r => new { r.BookId, r.UserId });

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