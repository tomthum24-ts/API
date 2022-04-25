//namespace API.Domain
//{
//    public interface IUnitOfWorkTransaction : IDisposable
//    {
//        ActionCollection ActionsBeforeCommit { get; set; }

//        ActionCollection ActionsAfterCommit { get; set; }

//        ActionCollection ActionsBeforeRollback { get; set; }

//        ActionCollection ActionsAfterRollback { get; set; }

//        Guid TransactionId { get; }
//        DbTransaction DbTransaction { get; }

//        bool HasActiveTransaction { get; }

//        void Commit();

//        void Rollback();

//        Task CommitAsync(CancellationToken cancellationToken = default);

//        Task RollbackAsync(CancellationToken cancellationToken = default);
//    }
//}
