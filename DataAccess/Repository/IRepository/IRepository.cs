using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T - Model
        IEnumerable<T> ReadAll();
        T ReadFirstOrDefault(Expression<Func<T, bool>> filter);
        void Create(T entity);
        void Delete(T entity);
        void DeleteMany(IEnumerable<T> entity);
    }
}