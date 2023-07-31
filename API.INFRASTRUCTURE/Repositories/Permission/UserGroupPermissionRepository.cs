using API.DOMAIN.DomainObjects.Permission;
using API.INFRASTRUCTURE.DataConnect;

namespace API.INFRASTRUCTURE.Repositories.Permission
{
    public class UserGroupPermissionRepository : RepositoryBase<UserGroupPermissions>, IUserGroupPermissionRepository
    {
        public UserGroupPermissionRepository(IDbContext db) : base(db)
        {
        }
    }
}