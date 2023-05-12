

using API.APPLICATION.Parameters.Location;
using API.APPLICATION.ViewModels.ByIdViewModel;
using API.APPLICATION.ViewModels.Location;
using API.DOMAIN.DTOs.Location;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.Response;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.Location
{
    public interface IDistrictServices
    {
        Task<PagingItems<DistrictDTO>> GetDistrictAsync(DistrictFilterParam param);
        Task<IEnumerable<ResponseByIdViewModel>> GetInfoByIdAsync(RequestByIdViewModel param);
    }
    public class DistrictServices : IDistrictServices
    {
        public readonly DapperContext _context;

        public DistrictServices(DapperContext context)
        {
            _context = context;
        }
        public async Task<PagingItems<DistrictDTO>> GetDistrictAsync(DistrictFilterParam param)
        {
            var result = new PagingItems<DistrictDTO>
            {
                PagingInfo = new PagingInfoDto
                {
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize,
                }
            };
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DM_LC_GetListDistrict_SelectWithPaging", param, commandType: CommandType.StoredProcedure);
            result.Items = await rs.ReadAsync<DistrictDTO>().ConfigureAwait(false);
            result.PagingInfo.TotalItems = await rs.ReadSingleAsync<int>().ConfigureAwait(false);
            return result;
        }
        public async Task<IEnumerable<ResponseByIdViewModel>> GetInfoByIdAsync(RequestByIdViewModel param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_LC_GetDistrictByIdReplace", param, commandType: System.Data.CommandType.StoredProcedure);
            var result = await rs.ReadAsync<ResponseByIdViewModel>().ConfigureAwait(false);
            return result; ;
        }

    }
}
