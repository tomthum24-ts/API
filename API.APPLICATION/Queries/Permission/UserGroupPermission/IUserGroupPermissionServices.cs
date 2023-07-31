using API.DOMAIN.DTOs.Permission;
using API.INFRASTRUCTURE.DataConnect;

namespace API.APPLICATION.Queries.GroupPermission
{
    public interface IUserGroupPermissionServices : IDanhMucQueries<UserGroupPermissionDTO>
    {
    }

    public class UserGroupPermissionServices : DanhMucQueries<UserGroupPermissionDTO>, IUserGroupPermissionServices
    {
        public UserGroupPermissionServices(DapperContext connectionFactory) : base(connectionFactory)
        {
        }
    }
}