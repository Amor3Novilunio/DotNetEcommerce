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
        IEnumerable<T> ReadAll(string? includeProps = null);
        T ReadFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProps = null);
        void Create(T entity);
        void Delete(T entity);
        void DeleteMany(IEnumerable<T> entity);
    }
}