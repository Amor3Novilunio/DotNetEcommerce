using DataAccess.Repository.IRepository;
using Ecommerce.DataAccess.ApplicationDbContext;
using Ecommerce.Models;

namespace DataAccess.Repository;

public class UsersRepository : Repository<User>, IUsersRepository
{
    private readonly ApplicationDbContext _db;
    public UsersRepository(ApplicationDbContext db) : base(db){
        _db = db;
    }

}
