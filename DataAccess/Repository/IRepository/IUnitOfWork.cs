using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        CategoriesRepository Category { get; }
        ProductsRepository Product { get; }
        UsersRepository User { get; }

        void Save();
    }
}