using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;

namespace Ecommerce.DataAccess.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Marvel", DisplayOrder = 1 },
                new Category { Id = 2, Name = "DC", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Sci-Fi", DisplayOrder = 3 }
            );
        }
    }
}