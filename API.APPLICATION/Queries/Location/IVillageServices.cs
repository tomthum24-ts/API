using API.APPLICATION.Parameters.Location;
using API.APPLICATION.ViewModels.ByIdViewModel;
using API.DOMAIN.DTOs.Location;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.Location
{
    public interface IVillageServices
    {
        Task<PagingItems<VillageDTO>> GetAllVillagePaging(VillageFilterParam param);
        Task<IEnumerable<ResponseByIdViewModel>> GetInfoByIdAsync(RequestByIdViewModel param);
    }
    public class VillageServices : IVillageServices
    {
        public readonly DapperContext _context;

        public VillageServices(DapperContext context)
        {
            _context = context;
        }
        public async Task<PagingItems<VillageDTO>> GetAllVillagePaging(VillageFilterParam param)
        {
            var result = new PagingItems<VillageDTO>
            {
                PagingInfo = new PagingInfoDto
                {
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize,
                }
            };
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DM_LC_GetListVillage_SelectWithPaging", param, commandType: CommandType.StoredProcedure);
            result.Items = await rs.ReadAsync<VillageDTO>().ConfigureAwait(false);
            result.PagingInfo.TotalItems = await rs.ReadSingleAsync<int>().ConfigureAwait(false);
            return result;
        }
        public async Task<IEnumerable<ResponseByIdViewModel>> GetInfoByIdAsync(RequestByIdViewModel param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_LC_GetVillageByIdReplace", param, commandType: System.Data.CommandType.StoredProcedure);
            var result = await rs.ReadAsync<ResponseByIdViewModel>().ConfigureAwait(false);
            return result; ;
        }
    }
}
