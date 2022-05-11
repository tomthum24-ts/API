using API.Data;
using API.DomainObjects;
using API.InterFace;

namespace API.Repositories.ACL
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext db) : base(db)
        {
        }
    }
}
