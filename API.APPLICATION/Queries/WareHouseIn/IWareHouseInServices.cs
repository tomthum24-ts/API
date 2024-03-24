using API.APPLICATION.Parameters.WareHouseIn;
using API.APPLICATION.ViewModels.WareHouseInDetail;
using API.DOMAIN.DTOs.WareHouseIn;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.Response;
using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.WareHouseIn
{
    public interface IWareHouseInServices
    {
        Task<PagingItems<WareHouseInDTO>> GetWareHouseInPagingAsync(WareHouseInFilterParam param);

        Task<WareHouseInDetailViewModel> GetWareHouseInByIdAsync(WareHouseInByIdParam param);
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
            param.IdUser = _userSessionInfo?.ID.Value ?? 0;
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
        public async Task<WareHouseInDetailResponseViewModel> GetDataWareHouseInByIdAsync(WareHouseInByIdParam param)
        {
            var result = new WareHouseInDetailResponseViewModel();
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DA_GetInfoWareHouseInById", param, commandType: CommandType.StoredProcedure);
            result = await rs.ReadFirstOrDefaultAsync<WareHouseInDetailResponseViewModel>().ConfigureAwait(false);
            result.WareHouseInDetailResponseDTOs = await rs.ReadAsync<WareHouseInDetailResponseDTO>().ConfigureAwait(false);
            return result;
        }

        public async Task<WareHouseInDetailViewModel> GetWareHouseInByIdAsync(WareHouseInByIdParam param)
        {
            var result = new WareHouseInDetailViewModel();
            var data = await GetDataWareHouseInByIdAsync(param);
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
            result.CarNumber=data.CarNumber;
            result.Container= data.Container;
            result.Door= data.Door; 
            result.Deliver= data.Deliver;
            result.Veterinary= data.Veterinary;
            result.Cont= data.Cont;
            result.NumberCode= data.NumberCode;
            result.InvoiceNumber = data.InvoiceNumber;
            result.TimeStart= data.TimeStart;
            result.TimeEnd= data.TimeEnd;
            result.WareHouseInDetailModels = data?.WareHouseInDetailResponseDTOs.GroupBy(x => x?.GuildId)?
                                                   .Select(y => new WareHouseInDetailModel
                                                   {
                                                       Id = y.First().Id,
                                                       RangeOfVehicle = y.First().RangeOfVehicle,
                                                       GuildId = y.First().GuildId,

                                                       VehicleDetaiModels = data?.WareHouseInDetailResponseDTOs.Where(e => e.GuildId == y.First().GuildId).GroupBy(c => c?.Id)?.Select(z => new VehicleDetaiModel
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