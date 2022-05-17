using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Services.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.INFRASTRUCTURE.Repositories.User
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly AppDbContext _db;

        protected RepositoryBase(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<T> FindAll() => _db.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _db.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => _db.Set<T>().Add(entity);
        public void Update(T entity) => _db.Set<T>().Update(entity);
        public void Delete(T entity) => _db.Set<T>().Remove(entity);
        public void UpdateRange(IEnumerable<T> entities) => _db.Set<T>().UpdateRange(entities);
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate = null,
            bool isIncludeDeleted = false,
            bool isTracking = false,
            params Expression<Func<T, object>>[] includeProperties) => _db.Set<T>().AsNoTracking();


    }
}
