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
        public CategoriesRepository Category {get; private set;}
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoriesRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}