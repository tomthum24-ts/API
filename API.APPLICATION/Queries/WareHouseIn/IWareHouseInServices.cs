using API.APPLICATION.Parameters.Customer;
using API.APPLICATION.Parameters.WareHouseIn;
using API.APPLICATION.ViewModels.WareHouseInDetail;
using API.DOMAIN.DTOs.Customer;
using API.DOMAIN.DTOs.WareHouseIn;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.Response;
using Dapper;
using Microsoft.AspNetCore.Connections;
using System.Data;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.WareHouseIn
{
    public interface IWareHouseInServices
    {
         Task<PagingItems<WareHouseInDTO>> GetWareHouseInPagingAsync(WareHouseInFilterParam param);
        Task<WareHouseInDetailResponseViewModel> GetWareHouseInByIdAsync(WareHouseInByIdParam param);
    }

    public class WareHouseInServices : IWareHouseInServices
    {
        public readonly DapperContext _context;
        private IUserSessionInfo _userSessionInfo;
        public WareHouseInServices(DapperContext context, IUserSessionInfo userSessionInfo)
        {
            _context = context;
            _userSessionInfo = userSessionInfo;
        }
        public async Task<PagingItems<WareHouseInDTO>> GetWareHouseInPagingAsync(WareHouseInFilterParam param)
        {
            var result = new PagingItems<WareHouseInDTO>
            {
                PagingInfo = new PagingInfoDto
                {
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize,
                }
            };
            param.IdUser= _userSessionInfo?.ID.Value ?? 0;
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DM_LC_GetListWareHouseIn_SelectWithPaging", param, commandType: CommandType.StoredProcedure);
            result.Items = await rs.ReadAsync<WareHouseInDTO>().ConfigureAwait(false);
            result.PagingInfo.TotalItems = await rs.ReadSingleAsync<int>().ConfigureAwait(false);
            return result;
        }
        //public async Task<WareHouseInByIdDTO> GetInfoUserByIdAsync(WareHouseInByIdParam param)
        //{
        //    var conn = _context.CreateConnection();
        //    using var rs = await conn.QueryMultipleAsync("SP_DA_GetInfoWareHouseInById", param, commandType: CommandType.StoredProcedure);
        //    var result = await rs.ReadFirstOrDefaultAsync<WareHouseInByIdDTO>().ConfigureAwait(false);
        //    return result;
        //}
        public async Task<WareHouseInDetailResponseViewModel> GetWareHouseInByIdAsync(WareHouseInByIdParam param)
        {
            var result = new WareHouseInDetailResponseViewModel();
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DA_GetInfoWareHouseInById", param, commandType: CommandType.StoredProcedure);
            result = await rs.ReadFirstOrDefaultAsync<WareHouseInDetailResponseViewModel>().ConfigureAwait(false);
            result.WareHouseInDetailResponseDTOs = await rs.ReadAsync<WareHouseInDetailResponseDTO>().ConfigureAwait(false);
            return result;
        }
    }
}