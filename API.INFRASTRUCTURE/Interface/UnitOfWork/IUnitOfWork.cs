using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.Interface.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int Complete();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
