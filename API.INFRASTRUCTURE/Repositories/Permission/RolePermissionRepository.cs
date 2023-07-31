using API.DOMAIN;
using API.INFRASTRUCTURE.DataConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.Repositories.Permission
{

    public class RolePermissionRepository : RepositoryBase<RolePermissions>, IRolePermissionRepository
    {
        public RolePermissionRepository(IDbContext db) : base(db)
        {
        }
    }
}
