using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;
using Ecommerce.DataAccess.ApplicationDbContext;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public CategoriesRepository Category {get; private set; }
        public ProductsRepository Product { get; private set; }
        public UsersRepository User { get; private set; }

        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoriesRepository(_db);
            Product = new ProductsRepository(_db);
            User = new UsersRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}