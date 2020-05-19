using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Repository.Interfaces;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.DataAccess.Repository.Implementations
{
    public class Repository<T>  : IRepository<T> where T : BaseEntity
    {
        protected readonly RestaurantContext _context;
        internal DbSet<T> dbSet;

        public Repository(RestaurantContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        
        public void Add(T entity)
        {
            dbSet.Add(entity);

        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, Func<System.Linq.IQueryable<T>, System.Linq.IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //properties splited by comma
            if (includeProperties != null)
            {
                foreach (var prop in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //properties splited by comma
            if (includeProperties != null)
            {
                foreach (var prop in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            var entity = dbSet.Find(id);

            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
