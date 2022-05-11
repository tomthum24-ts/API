using API.Data;
using API.InterFace;
using API.Repositories.ACL;

namespace API.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext _db;
        private IUserRepository _user;

        public RepositoryWrapper(IUserRepository user, AppDbContext db)
        {
            _user = user;
            _db = db;
        }
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_db);
                }
                return _user;
            }
        }
        public RepositoryWrapper(AppDbContext repositoryContext)
        {
            _db = repositoryContext;
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
