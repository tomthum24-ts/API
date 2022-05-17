using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.INFRASTRUCTURE.Services.User
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void UpdateRange(IEnumerable<T> entities);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null,
            bool isIncludeDeleted = false,
            bool isTracking = false,
            params Expression<Func<T, object>>[] includeProperties);

    }
}
