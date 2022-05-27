
namespace API.INFRASTRUCTURE.Services.User
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        void Save();
    }
}
