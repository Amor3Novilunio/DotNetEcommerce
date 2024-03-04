using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;
using Ecommerce.DataAccess.ApplicationDbContext;
using Ecommerce.Models;

namespace DataAccess.Repository
{
    public class CategoriesRepository : Repository<Category>, ICategoriesRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoriesRepository(ApplicationDbContext db) : base(db) {
             _db = db;
        }
        public void Update(Category objData)
        {
            _db.Categories.Update(objData);
        }
    }
}