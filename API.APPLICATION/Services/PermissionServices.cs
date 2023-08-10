using API.APPLICATION.Parameters.Permission;
using API.DOMAIN.DTOs.Permission;
using API.INFRASTRUCTURE.DataConnect;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public interface IRolePermissionQueries
    {
        Task<IEnumerable<PermissionDTO>> GetAllPermissionNotPagingAsync(PermissionFilterParam param);
        Task<IEnumerable<AllPermissionDTO>> GetAllPermissionAsync();
        Task<IEnumerable<PermissionByIdDTO>> GetPermissionByIdAsync(PermissionByIdFilterParam param);
    }
    public class RolePermissionQueries : IRolePermissionQueries
    {
        public readonly DapperContext _context;


        public RolePermissionQueries(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PermissionDTO>> GetAllPermissionNotPagingAsync(PermissionFilterParam param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_PM_GetListPermission", param, commandType: CommandType.StoredProcedure);
            var result = await rs.ReadAsync<PermissionDTO>().ConfigureAwait(false);
            return result;
        }
        public async Task<IEnumerable<AllPermissionDTO>> GetAllPermissionAsync()
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_PhanQuyen_GetAllQuyen", null , commandType: CommandType.StoredProcedure);
            var result = await rs.ReadAsync<AllPermissionDTO>().ConfigureAwait(false);
            return result;
        }
        public async Task<IEnumerable<PermissionByIdDTO>> GetPermissionByIdAsync(PermissionByIdFilterParam param )
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_PhanQuyen_GetPhanQuyenById", param, commandType: CommandType.StoredProcedure);
            var result = await rs.ReadAsync<PermissionByIdDTO>().ConfigureAwait(false);
            return result;
        }
    }
}
