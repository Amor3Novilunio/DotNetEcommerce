using DataAccess.Repository.IRepository;
using Ecommerce.DataAccess.ApplicationDbContext;
using Ecommerce.Models;

namespace DataAccess.Repository;

public class ProductsRepository : Repository<Product>, IProductsRepository
{
    private readonly ApplicationDbContext _db;
    public ProductsRepository(ApplicationDbContext db) : base(db){
        _db = db;
    }
}
