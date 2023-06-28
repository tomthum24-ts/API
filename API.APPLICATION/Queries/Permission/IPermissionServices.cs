using API.APPLICATION.Parameters.Permission;
using API.DOMAIN.DTOs.Permission;
using API.INFRASTRUCTURE.DataConnect;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public interface IPermissionServices
    {
        Task<IEnumerable<PermissionDTO>> GetAllPermissionNotPaging(PermissionFilterParam param);
    }
    public class PermissionServices : IPermissionServices
    {
        public readonly DapperContext _context;


        public PermissionServices(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PermissionDTO>> GetAllPermissionNotPaging(PermissionFilterParam param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_PM_GetListPermission", param, commandType: CommandType.StoredProcedure);
            var result = await rs.ReadAsync<PermissionDTO>().ConfigureAwait(false);
            return result;
        }

    }
}
