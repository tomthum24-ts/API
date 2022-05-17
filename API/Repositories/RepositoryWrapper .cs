using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.DataConnect;
using API.InterFace;

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
