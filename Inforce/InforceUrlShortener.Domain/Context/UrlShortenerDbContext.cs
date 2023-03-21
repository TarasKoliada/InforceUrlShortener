using InforceUrlShortener.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InforceUrlShortener.Domain.Context
{
    public class UrlShortenerDBContext : DbContext
    {
        public UrlShortenerDBContext(DbContextOptions options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortUrl>()
                .HasIndex(u => u.OriginalUrl)
                .IsUnique();
        }
        public DbSet<ShortUrl> ShortUrls { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
