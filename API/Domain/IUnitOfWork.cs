using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        #region Save

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAndDispatchDomainEventsAsync(CancellationToken cancellationToken = default);

        #endregion Save
    }
}
