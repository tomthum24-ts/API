using API.Data;
using API.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace API.Domain
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        protected readonly IServiceProvider ServiceProvider;
        protected ConcurrentDictionary<Type, object> Repositories = new ConcurrentDictionary<Type, object>();

        public IUserRepository Users { get; private set; }

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Users = new UserRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public virtual IEntityRepository<T, TKey> GetEntityRepository<T, TKey>() where T : Entity<TKey> where TKey : struct
        {
            if (!Repositories.TryGetValue(typeof(IEntityRepository<T, TKey>), out var repository))
            {
                Repositories[typeof(IEntityRepository<T, TKey>)] = repository = ServiceProvider.GetRequiredService<IEntityRepository<T, TKey>>();
            }

            return repository as IEntityRepository<T, TKey>;
        }
    }
}
