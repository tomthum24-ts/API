using API.DOMAIN.DTOs.Permission;
using API.INFRASTRUCTURE.DataConnect;

namespace API.APPLICATION.Queries.Permission.RolePermission
{
    public interface IRolePermissonServices : IDanhMucQueries<RolePermissionDTO>
    {
    }

    public class RolePermissonServices : DanhMucQueries<RolePermissionDTO>, IRolePermissonServices
    {
        public RolePermissonServices(DapperContext connectionFactory) : base(connectionFactory)
        {
        }
    }
}