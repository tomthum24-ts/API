using API.DOMAIN;
using API.INFRASTRUCTURE.DataConnect;

namespace API.INFRASTRUCTURE.Repositories.Permission
{
    public class CredentialRepository : RepositoryBase<PM_Credential>, ICredentialRepository
    {
        public CredentialRepository(IDbContext db) : base(db)
        {
        }
    }
}