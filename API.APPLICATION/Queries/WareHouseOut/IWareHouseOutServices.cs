using API.APPLICATION.Parameters.WareHouseIn;
using API.APPLICATION.Parameters;
using API.APPLICATION.Parameters.WareHouseOut;
using API.APPLICATION.Parameters.WareHouseOut;
using API.APPLICATION.ViewModels.BieuMau;
using API.APPLICATION.ViewModels.WareHouseIn;
using API.APPLICATION.ViewModels.WareHouseOutDetail;
using API.APPLICATION.ViewModels.WareHouseOutDetail;
using API.DOMAIN;
using API.DOMAIN.DTOs.WareHouseOut;
using API.DOMAIN.DTOs.WareHouseOut;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.Report;
using BaseCommon.Common.Response;
using BaseCommon.Utilities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BaseCommon.Common.Report.Interfaces;
using API.APPLICATION.ViewModels.WareHouseOut;

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
            result.Pallet=data.Pallet;
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
                                                           RONumber= z.First().RONumber,
                                                       }
                                                       )
                                                   });

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