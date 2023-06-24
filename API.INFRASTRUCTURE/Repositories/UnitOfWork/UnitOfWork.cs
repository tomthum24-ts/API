using API.DOMAIN;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using BaseCommon.Common.ClaimUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IUserSessionInfo  _userSessionInfo;
        private readonly IDbContext _context;
        public UnitOfWork(IDbContext context, IUserSessionInfo userSessionInfo)
        {
            _context = context;
            Users = new UserRepository(_context);
            _userSessionInfo = userSessionInfo;
        }
        public IUserRepository Users { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            StandardizeEntities();
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result;
        }
        protected  void StandardizeEntities()
        {
            var dateTimeNow = DateTime.UtcNow;

            var listState = new List<EntityState>
            {
                EntityState.Added,
                EntityState.Modified
            };

            var listEntryAddUpdate = _context.ChangeTracker.Entries()
                .Where(x => listState.Contains(x.State))
                .Select(x => x)
                .ToList();

            foreach (var entry in listEntryAddUpdate)
            {
                //if (entry.Entity is BaseEntity baseEntity)
                //{
                //    if (entry.State == EntityState.Added)
                //    {
                //        baseEntity.DeleteDate = null;

                //        baseEntity.UpdateDate = null;

                //        baseEntity.CreatedDate = dateTimeNow;
                //    }
                //    else
                //    {
                //        if (baseEntity.IsDelete != null && baseEntity.IsDelete == true)
                //        {
                //            baseEntity.DeleteDate = dateTimeNow;
                //        }
                //        else
                //        {
                //            baseEntity.UpdateDate = dateTimeNow;
                //        }
                //    }
                //}

                if (entry.Entity is APIEntity entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedById = _userSessionInfo.ID;
                        entity.CreationDate = dateTimeNow;
                    }
                    else
                    {
                        if (entity.IsDelete != null && entity.IsDelete == true)
                        {
                            entity.DeletedById = _userSessionInfo.ID;
                            entity.DeletionDate = dateTimeNow;
                        }
                        else
                        {
                            entity.UpdatedById = _userSessionInfo.ID;
                            entity.UpdateDate = dateTimeNow;
                        }
                    }
                }
            }
        }
    }
}
