using API.Repositories;
using System.Threading.Tasks;

namespace API.Domain
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        Task CompleteAsync();

    }
}
