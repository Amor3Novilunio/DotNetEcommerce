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
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Canned Food", DisplayOrder = 1, CreatedBy = "Amor" },
                new Category { Id = 2, Name = "Soda", DisplayOrder = 2, CreatedBy = "Amor" },
                new Category { Id = 3, Name = "Movies", DisplayOrder = 3, CreatedBy = "Amor" }
            );
        }
    }
}