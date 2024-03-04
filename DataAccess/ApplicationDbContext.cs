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
                new Category { Id = 1, Name = "Canned Food", DisplayOrder = 1, Createdby = "Amor" },
                new Category { Id = 2, Name = "Soda", DisplayOrder = 2, Createdby = "Amor" },
                new Category { Id = 3, Name = "Movies", DisplayOrder = 3, Createdby = "Amor" }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Tuna", Description = "Canned Food", Price = 30, Createdby = "Amor", Category = "Canned Food" },
                new Product { Id = 2, Name = "Godzilla", Description = "Big Giant Kaiju", Price = 35, Createdby = "Amor", Category = "Movies" },
                new Product { Id = 3, Name = "Coke", Description = "Soda", Price = 40, Createdby = "Amor", Category = "Soda" }
            );
        }
    }
}