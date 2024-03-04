using Ecommerce.Models;

namespace DataAccess.Repository.IRepository;

public interface IProductsRepository : IRepository<Product>
{
     void Update(Product entity);
}
