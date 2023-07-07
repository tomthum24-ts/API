using API.DOMAIN;
using API.INFRASTRUCTURE.DataConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.Repositories.Permission
{
    public class CredentialRepository : RepositoryBase<PM_Credential>, ICredentialRepository
    {
        public CredentialRepository(IDbContext db) : base(db)
        {
        }
    }
}
