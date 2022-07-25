using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.INFRASTRUCTURE
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindById(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity, bool isPhysicalDelete = false);
        void UpdateRange(IEnumerable<T> entities);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null,
            bool isIncludeDeleted = false,
            bool isTracking = false,
            params Expression<Func<T, object>>[] includeProperties);
        void AddRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities, bool isPhysicalDelete = false);
    }
}
