using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;
using Ecommerce.DataAccess.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db) {
            _db = db;
            // dbset is _db.databaseName
            dbSet=_db.Set<T>();
        }
        public void Create(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteMany(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public IEnumerable<T> ReadAll(string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            if(!string.IsNullOrEmpty(includeProps))
            {
                foreach(var incProps in includeProps
                    .Split(new char[','] , StringSplitOptions.RemoveEmptyEntries))
                {
                   query = query.Include(incProps);
                }
            }
            return query.ToList();
        }

        public T ReadFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var incProps in includeProps.Split(new char[','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incProps);
                }
            }
            return query.FirstOrDefault()!;
        }
    }
}