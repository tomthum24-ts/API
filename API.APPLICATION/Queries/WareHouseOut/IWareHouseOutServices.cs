using API.APPLICATION.Parameters.WareHouseOut;
using API.APPLICATION.Parameters.WareHouseOut;
using API.APPLICATION.ViewModels.WareHouseOutDetail;
using API.APPLICATION.ViewModels.WareHouseOutDetail;
using API.DOMAIN.DTOs.WareHouseOut;
using API.DOMAIN.DTOs.WareHouseOut;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.Response;
using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.WareHouseOut
{
    public interface IWareHouseOutServices
    {
        Task<PagingItems<WareHouseOutDTO>> GetWareHouseOutPagingAsync(WareHouseOutFilterParam param);

        Task<WareHouseOutDetailViewModel> GetWareHouseOutByIdAsync(WareHouseOutByIdParam param);
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
        public async Task<WareHouseOutDetailResponseViewModel> GetDataWareHouseOutByIdAsync(WareHouseOutByIdParam param)
        {
            var result = new WareHouseOutDetailResponseViewModel();
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DA_GetInfoWareHouseOutById", param, commandType: CommandType.StoredProcedure);
            result = await rs.ReadFirstOrDefaultAsync<WareHouseOutDetailResponseViewModel>().ConfigureAwait(false);
            result.WareHouseOutDetailResponseDTOs = await rs.ReadAsync<WareHouseOutDetailResponseDTO>().ConfigureAwait(false);
            return result;
        }

        public async Task<WareHouseOutDetailViewModel> GetWareHouseOutByIdAsync(WareHouseOutByIdParam param)
        {
            var result = new WareHouseOutDetailViewModel();
            var data = await GetDataWareHouseOutByIdAsync(param);
            result.Id = data.Id;
            result.Code = data.Code;
            result.DateCode = data.DateCode;
            result.Representative = data.Representative;
            result.IntendTime = data.IntendTime;
            result.WareHouseName = data.WareHouseName;
            result.Note = data.Note;
            result.OrtherNote = data.OrtherNote;
            result.FileAttach = data.FileAttach;
            result.CreatedById = data.CreatedById;
            result.CreateUser = data.CreateUser;
            result.CustomerName = data.CustomerName;
            result.FileName = data.FileName;
            result.FileId = data.FileId;
            result.Seal = data.Seal;
            result.Temp = data.Temp;
            result.CarNumber = data.CarNumber;
            result.Container = data.Container;
            result.Door = data.Door;
            result.Deliver = data.Deliver;
            result.Veterinary = data.Veterinary;
            result.Cont = data.Cont;
            result.NumberCode = data.NumberCode;
            result.InvoiceNumber = data.InvoiceNumber;
            result.TimeStart = data.TimeStart;
            result.TimeEnd = data.TimeEnd;
            result.WareHouseOutDetailModels = data?.WareHouseOutDetailResponseDTOs.GroupBy(x => x?.GuildId)?
                                                   .Select(y => new WareHouseOutDetailModel
                                                   {
                                                       Id = y.First().Id,
                                                       RangeOfVehicle = y.First().RangeOfVehicle,
                                                       GuildId = y.First().GuildId,

                                                       VehicleDetaiModels = data?.WareHouseOutDetailResponseDTOs.Where(e => e.GuildId == y.First().GuildId).GroupBy(c => c?.Id)?.Select(z => new VehicleDetaiModel
                                                       {
                                                           ProductId = z.First().ProductId,
                                                           QuantityProduct = z.First().QuantityProduct,
                                                           Unit = z.First().Unit,
                                                           Size = z.First().Size,
                                                           Weight = z.First().Weight,
                                                       }
                                                       )
                                                   });

            return result;
        }
    }
}