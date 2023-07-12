using BaseCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseCommon.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //IUserRepository Users { get; }
        int Complete();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        #region Action

        ActionCollection ActionsBeforeSaveChanges { get; }

        ActionCollection ActionsAfterSaveChanges { get; }

        ActionCollection ActionsBeforeCommit { get; }

        ActionCollection ActionsAfterCommit { get; }

        ActionCollection ActionsBeforeRollback { get; }

        ActionCollection ActionsAfterRollback { get; }

        #endregion Action
    }
}
