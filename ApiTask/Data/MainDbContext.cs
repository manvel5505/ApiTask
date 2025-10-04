using ApiTask.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
        public DbSet<RequestEntity> Requests { get; set; }
        public DbSet<CombinationEntity> Combinations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestEntity>()
                .Property(r => r.InputItems)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<CombinationEntity>()
                .Property(c => c.Items)
                .HasColumnType("nvarchar(max)");
        }
    }
}
