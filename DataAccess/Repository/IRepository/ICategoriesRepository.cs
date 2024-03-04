using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;

namespace DataAccess.Repository.IRepository
{
    public interface ICategoriesRepository : IRepository<Category> 
    {
        void Update(Category objData);
    }
}