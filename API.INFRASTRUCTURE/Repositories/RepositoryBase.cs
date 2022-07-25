using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.INFRASTRUCTURE.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private readonly AppDbContext _db;
        private DbSet<T> _dbSet;

        protected RepositoryBase(AppDbContext db)
        {
            _db = db;
        }
        protected DbSet<T> DbSet
        {
            get
            {
                if (_dbSet != null)
                {
                    return _dbSet;
                }

                _dbSet = _db.Set<T>();

                return _dbSet;
            }
        }

       
        #region AddItem
        public void Add(T entity) => _db.Set<T>().Add(entity);
        public void AddRange(IEnumerable<T> entities)
        {
            _db.Set<T>().AddRange(entities);
        }
        #endregion

        #region UpdateItem
        public void Update(T entity) => _db.Set<T>().Update(entity);
        public void UpdateRange(IEnumerable<T> entities) => _db.Set<T>().UpdateRange(entities);
        #endregion

        #region DeleteItem
        public void Delete(T entity, bool isPhysicalDelete = false)
        {
            Delete(entity, isPhysicalDelete);
        }
        public void DeleteRange(IEnumerable<T> entities, bool isPhysicalDelete = false)
        {
            foreach (var entity in entities)
            {
                Remove(entity, isPhysicalDelete);
            }
        }
        public virtual void Remove(T entity, bool isPhysicalDelete = false)
        {
            try
            {
                TryAttach(entity);

                if (!isPhysicalDelete)
                {
                    entity.IsDelete = true;

                    _db.Entry(entity).Property(x => x.DeletionDate).IsModified = true;

                    _db.Entry(entity).Property(x => x.IsDelete).IsModified = true;
                }
                else
                {
                    DbSet.Remove(entity);
                }
            }
            catch (Exception)
            {
                RefreshEntity(entity);

                throw;
            }
        }
        #endregion

        #region ReadItem
        public IQueryable<T> FindAll() => _db.Set<T>().AsNoTracking();
        public IQueryable<T> FindById(Expression<Func<T, bool>> expression) =>
            _db.Set<T>().Where(expression).AsNoTracking();
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate = null,bool isIncludeDeleted = false,bool isTracking = false,params Expression<Func<T, object>>[] includeProperties)
        {
            var query = DbSet.AsQueryable();

            if (isTracking) query = query.AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            includeProperties = includeProperties?.Distinct().ToArray();

            if (includeProperties?.Any() == true)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            // NOTE: Query Filter (query.IgnoreQueryFilters()), it affect to load data business logic.
            // Currently not flexible, please check https://github.com/aspnet/EntityFrameworkCore/issues/8576
            query = isIncludeDeleted ? query.IgnoreQueryFilters() : query.Where(x => x.IsDelete == false || x.IsDelete == null);

            return query;
        }

        #endregion

        protected void TryAttach(T entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
        }
        public  void RefreshEntity(T entity)
        {
            _db.Entry(entity).Reload();
        }


    }
}
