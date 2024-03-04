using Ecommerce.Models;

namespace DataAccess.Repository.IRepository;

public interface IUsersRepository : IRepository<User>
{
    void Update(User entity);
}
