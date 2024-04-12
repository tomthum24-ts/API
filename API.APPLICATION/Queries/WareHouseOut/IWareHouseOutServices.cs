using API.APPLICATION.Commands.Project;
using API.APPLICATION.Parameters;
using API.APPLICATION.Parameters.WareHouseIn;
using API.APPLICATION.Parameters.WareHouseOut;
using API.APPLICATION.ViewModels.BieuMau;
using API.APPLICATION.ViewModels.WareHouseOut;
using API.APPLICATION.ViewModels.WareHouseOutDetail;
using API.DOMAIN;
using API.DOMAIN.DTOs.WareHouseOut;
using API.INFRASTRUCTURE.DataConnect;
using AutoMapper;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.Report;
using BaseCommon.Common.Report.Interfaces;
using BaseCommon.Common.Response;
using BaseCommon.Utilities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.WareHouseOut
{
    public interface IWareHouseOutServices
    {
        Task<PagingItems<WareHouseOutDTO>> GetWareHouseOutPagingAsync(WareHouseOutFilterParam param);

        Task<WareHouseOutDetailViewModel> GetWareHouseOutByIdAsync(WareHouseOutByIdParam param);

        Task<BieuMauInfoResponseViewModel> ExportExcelWareHouseOutAsync(ReportWareHouseOutByIdReplaceViewModel request);
    }

    public class WareHouseOutServices : IWareHouseOutServices
    {
        public readonly DapperContext _context;
        private IUserSessionInfo _userSessionInfo;
        protected readonly ISYSBieuMauQueries _sysBieuMauQueries;
        protected readonly IReportQueries _reportQueries;
        private readonly IMapper _mapper;
        private readonly IExportService _exportService;

        public WareHouseOutServices(DapperContext context, IUserSessionInfo userSessionInfo, ISYSBieuMauQueries sysBieuMauQueries, IReportQueries reportQueries, IMapper mapper, IExportService exportService)
        {
            _context = context;
            _userSessionInfo = userSessionInfo;
            _sysBieuMauQueries = sysBieuMauQueries;
            _reportQueries = reportQueries;
            _mapper = mapper;
            _exportService = exportService;
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
            result.PagingInfo.TotalAll = await rs.ReadSingleAsync<int>().ConfigureAwait(false);
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
            //result = await rs.ReadFirstOrDefaultAsync<WareHouseOutDetailResponseViewModel>().ConfigureAwait(false);
            result.WareHouseOutResponseDTOs = await rs.ReadAsync<WareHouseOutResponseDTO>().ConfigureAwait(false);
            result.WareHouseOutDetailResponseDTOs = await rs.ReadAsync<WareHouseOutDetailResponseDTO>().ConfigureAwait(false);
            result.WareOutHouseFileAttachDTOs = await rs.ReadAsync<WareHouseOutFileAttachDTO>().ConfigureAwait(false);
            return result;
        }

        public async Task<WareHouseOutDetailViewModel> GetWareHouseOutByIdAsync(WareHouseOutByIdParam param)
        {
            var result = new WareHouseOutDetailViewModel();
            var data = await GetDataWareHouseOutByIdAsync(param);
            var wareHouseOut = data.WareHouseOutResponseDTOs?.FirstOrDefault();
            result.Id = wareHouseOut.Id;
            result.Code = wareHouseOut.Code;
            result.DateCode = wareHouseOut.DateCode;
            result.Representative = wareHouseOut.Representative;
            result.IntendTime = wareHouseOut.IntendTime;
            result.WareHouseName = wareHouseOut.WareHouseName;
            result.Note = wareHouseOut.Note;
            result.OrtherNote = wareHouseOut.OrtherNote;
            result.FileAttach = wareHouseOut.FileAttach;
            result.CreatedById = wareHouseOut.CreatedById;
            result.CreateUser = wareHouseOut.CreateUser;
            result.CustomerName = wareHouseOut.CustomerName;
            result.FileName = wareHouseOut.FileName;
            result.FileId = wareHouseOut.FileId;
            result.Seal = wareHouseOut.Seal;
            result.Temp = wareHouseOut.Temp;
            result.CarNumber = wareHouseOut.CarNumber;
            result.Container = wareHouseOut.Container;
            result.Door = wareHouseOut.Door;
            result.Deliver = wareHouseOut.Deliver;
            result.Veterinary = wareHouseOut.Veterinary;
            result.Cont = wareHouseOut.Cont;
            result.NumberCode = wareHouseOut.NumberCode;
            result.InvoiceNumber = wareHouseOut.InvoiceNumber;
            result.TimeStart = wareHouseOut.TimeStart;
            result.TimeEnd = wareHouseOut.TimeEnd;
            result.Pallet = wareHouseOut.Pallet;
            result.Status= wareHouseOut.Status;
            result.AddressCustomer= wareHouseOut.AddressCustomer;
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
                                                           Note = z.First().Note,
                                                           LotNo = z.First().LotNo,
                                                           TotalWeighScan = z.First().TotalWeighScan,
                                                           ProductDate = z.First().ProductDate,
                                                           ExpiryDate = z.First().ExpiryDate,
                                                           RONumber = z.First().RONumber,
                                                           MadeIn = z.First().MadeIn,
                                                           ProductName=z.First().ProductName,
                                                           UnitName=z.First().UnitName
                                                       }
                                                       )
                                                   });
            
            result.WareHouseOutFileAttachViewModels= _mapper.Map<List<WareHouseOutFileAttachViewModel>>(data.WareOutHouseFileAttachDTOs);
            return result;
        }

        public async Task<IEnumerable<ReportReplaceInfoHTMLDTO>> GetDataWareHouseOutReplaceThongTin(ReportReplaceWareHouseInParam param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_WareHouse_GetThongTinWareHouseOutByIdReplace", param, commandType: System.Data.CommandType.StoredProcedure);
            var result = await rs.ReadAsync<ReportReplaceInfoHTMLDTO>().ConfigureAwait(false);
            return result;
        }

        public async Task<BieuMauInfoResponseViewModel> ExportExcelWareHouseOutAsync(ReportWareHouseOutByIdReplaceViewModel request)
        {
            var param = _mapper.Map<WareHouseOutByIdParam>(request);
            var queryResult = await GetDataWareHouseOutByIdAsync(param).ConfigureAwait(false);
            var dicMailMerge = await GetDataWareHouseOutReplaceThongTin(new ReportReplaceWareHouseInParam { IdWareHouse = request.Id });
            Dictionary<string, string> replaceSameValues = dicMailMerge.ToDictionary(x => x.ObjKey, x => StringHelpers.Normalization(x.ObjValue));
            replaceSameValues.Add("NguoiXuatBan", "Hahaha");
            var bieuMau = await _sysBieuMauQueries.GetBieuMauByFilter(new SYSBieuMauFilterParam { MaBieuMau = ReportConstants.WareHouseOut_WHI003 });
            //ValidateBieuMau(bieuMau);

            MemoryStream outputStream;
            if (bieuMau.IsExportPDF)
            {
                outputStream = _exportService.ExportPdfFromExcel(queryResult.WareHouseOutDetailResponseDTOs,
                    bieuMau.MaBieuMau, bieuMau.NoiDung, bieuMau.TenBieuMau, replaceSameValues);
            }
            else
            {
                outputStream = _exportService.ExportExcelData(queryResult.WareHouseOutDetailResponseDTOs,
                    bieuMau.MaBieuMau, bieuMau.NoiDung, bieuMau.TenBieuMau, replaceSameValues);
            }

            BieuMauInfoResponseViewModel bieuMauResponse = new BieuMauInfoResponseViewModel();
            bieuMauResponse.OutputStream = outputStream;
            bieuMauResponse.ContentType = bieuMau.IsExportPDF ? ReportConstant.ContentTypeForPDF : _exportService.GetContentType(bieuMau.LoaiFile);
            bieuMauResponse.TenBieuMau = bieuMau.TenBieuMau + _exportService.GetExtensionFile(bieuMauResponse.ContentType);
            //await _bieuMauLogger.LogAsync(bieuMauResponse.TenBieuMau, HRMExcelConstants.TongHopKetQuaDanhGiaCongChucVienChuc_V10011, bieuMauResponse.ContentType, bieuMauResponse.OutputStream.ToArray());
            return bieuMauResponse;
        }
    }
}