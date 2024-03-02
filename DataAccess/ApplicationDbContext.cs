using Microsoft.EntityFrameworkCore;
using Models.Category;

namespace DataAccess.ApplicationDb
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories {get;set;} 
    }
}