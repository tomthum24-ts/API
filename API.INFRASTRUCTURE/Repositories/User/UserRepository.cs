using API.DOMAIN;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Repositories;

namespace API.INFRASTRUCTURE
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbContext db) : base(db)
        {
        }
    }
}
