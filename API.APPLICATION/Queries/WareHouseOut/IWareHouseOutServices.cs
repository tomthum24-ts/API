using API.APPLICATION.Parameters.WareHouseOut;
using API.APPLICATION.ViewModels.WareHouseOutDetail;
using API.DOMAIN.DTOs.WareHouseOut;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.Response;
using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.WareHouseOut
{
    public interface IWareHouseOutServices
    {
        Task<PagingItems<WareHouseOutDTO>> GetWareHouseOutPagingAsync(WareHouseOutFilterParam param);

        Task<WareHouseOutDetailResponseViewModel> GetWareHouseOutByIdAsync(WareHouseOutByIdParam param);
    }

    public class WareHouseOutServices : IWareHouseOutServices
    {
        public readonly DapperContext _context;
        private IUserSessionInfo _userSessionInfo;

        public WareHouseOutServices(DapperContext context, IUserSessionInfo userSessionInfo)
        {
            _context = context;
            _userSessionInfo = userSessionInfo;
        }

        public async Task<PagingItems<WareHouseOutDTO>> GetWareHouseOutPagingAsync(WareHouseOutFilterParam param)
        {
            var result = new PagingItems<WareHouseOutDTO>
            {
                PagingInfo = new PagingInfoDto
                {
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize,
                }
            };
            param.IdUser = _userSessionInfo?.ID.Value ?? 0;
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DM_LC_GetListWareHouseOut_SelectWithPaging", param, commandType: CommandType.StoredProcedure);
            result.Items = await rs.ReadAsync<WareHouseOutDTO>().ConfigureAwait(false);
            result.PagingInfo.TotalItems = await rs.ReadSingleAsync<int>().ConfigureAwait(false);
            return result;
        }

        //public async Task<WareHouseOutByIdDTO> GetInfoUserByIdAsync(WareHouseOutByIdParam param)
        //{
        //    var conn = _context.CreateConnection();
        //    using var rs = await conn.QueryMultipleAsync("SP_DA_GetInfoWareHouseOutById", param, commandType: CommandType.StoredProcedure);
        //    var result = await rs.ReadFirstOrDefaultAsync<WareHouseOutByIdDTO>().ConfigureAwait(false);
        //    return result;
        //}
        public async Task<WareHouseOutDetailResponseViewModel> GetWareHouseOutByIdAsync(WareHouseOutByIdParam param)
        {
            var result = new WareHouseOutDetailResponseViewModel();
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DA_GetInfoWareHouseOutById", param, commandType: CommandType.StoredProcedure);
            result = await rs.ReadFirstOrDefaultAsync<WareHouseOutDetailResponseViewModel>().ConfigureAwait(false);
            result.WareHouseOutDetailResponseDTOs = await rs.ReadAsync<WareHouseOutDetailResponseDTO>().ConfigureAwait(false);
            return result;
        }
    }
}