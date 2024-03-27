using API.APPLICATION.Parameters;
using API.APPLICATION.Parameters.WareHouseIn;
using API.APPLICATION.ViewModels.BieuMau;
using API.APPLICATION.ViewModels.ByIdViewModel;
using API.APPLICATION.ViewModels.WareHouseInDetail;
using API.DOMAIN.DTOs.User;
using API.DOMAIN;
using API.DOMAIN.DTOs.WareHouseIn;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.Report.Infrastructures;
using BaseCommon.Common.Report;
using BaseCommon.Common.Response;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BaseCommon.Common.Report.Interfaces;
using BaseCommon.Utilities;
using Microsoft.AspNetCore.Connections;
using API.APPLICATION.ViewModels.WareHouseIn;
using Aspose.Words;
using API.DOMAIN.DTOs.Product;

namespace API.APPLICATION.Queries.WareHouseIn
{
    public interface IWareHouseInServices
    {
        Task<PagingItems<WareHouseInDTO>> GetWareHouseInPagingAsync(WareHouseInFilterParam param);
        Task<WareHouseInDetailViewModel> GetWareHouseInByIdAsync(WareHouseInByIdParam param);
        Task<BieuMauInfoResponseViewModel> ExportWordThongTinAsync(ReportWareHouseInByIdReplaceViewModel request);
        Task<BieuMauInfoResponseViewModel> ExportExcelWareHouseInAsync(ReportWareHouseInByIdReplaceViewModel request);
    }

    public class WareHouseInServices : IWareHouseInServices
    {
        public readonly DapperContext _context;
        private IUserSessionInfo _userSessionInfo;
        protected readonly ISYSBieuMauQueries _sysBieuMauQueries;
        protected readonly IReportQueries _reportQueries;
        private readonly IMapper _mapper;
        private readonly IExportService _exportService;
        public WareHouseInServices(DapperContext context, IUserSessionInfo userSessionInfo, ISYSBieuMauQueries sysBieuMauQueries, IReportQueries reportQueries, IMapper mapper, IExportService exportService)
        {
            _context = context;
            _userSessionInfo = userSessionInfo;
            _sysBieuMauQueries = sysBieuMauQueries;
            _reportQueries = reportQueries;
            _mapper = mapper;
            _exportService = exportService;
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
            result.WareHouseInResponseDTOs = await rs.ReadAsync<WareHouseInResponseDTO>().ConfigureAwait(false);
            result.WareHouseInDetailResponseDTOs = await rs.ReadAsync<WareHouseInDetailResponseDTO>().ConfigureAwait(false);
            return result;
        }

        public async Task<WareHouseInDetailViewModel> GetWareHouseInByIdAsync(WareHouseInByIdParam param)
        {
            var result = new WareHouseInDetailViewModel();
            var data = await GetDataWareHouseInByIdAsync(param);
            var wareHouseIn= data.WareHouseInResponseDTOs?.FirstOrDefault();
            if(wareHouseIn == null)
            {
                return result;
            }
            result.Id = wareHouseIn.Id;
            result.Code = wareHouseIn.Code;
            result.DateCode = wareHouseIn.DateCode;
            result.Representative = wareHouseIn.Representative;
            result.IntendTime = wareHouseIn.IntendTime;
            result.WareHouseName = wareHouseIn.WareHouseName;
            result.Note = wareHouseIn.Note;
            result.OrtherNote = wareHouseIn.OrtherNote;
            result.FileAttach = wareHouseIn.FileAttach;
            result.CreatedById = wareHouseIn.CreatedById;
            result.CreateUser = wareHouseIn.CreateUser;
            result.CustomerName = wareHouseIn.CustomerName;
            result.FileName = wareHouseIn.FileName;
            result.FileId = wareHouseIn.FileId;
            result.Seal = wareHouseIn.Seal;
            result.Temp = wareHouseIn.Temp;
            result.CarNumber= wareHouseIn.CarNumber;
            result.Container= wareHouseIn.Container;
            result.Door= wareHouseIn.Door; 
            result.Deliver= wareHouseIn.Deliver;
            result.Veterinary= wareHouseIn.Veterinary;
            result.Cont= wareHouseIn.Cont;
            result.NumberCode= wareHouseIn.NumberCode;
            result.InvoiceNumber = wareHouseIn.InvoiceNumber;
            result.TimeStart= wareHouseIn.TimeStart;
            result.TimeEnd= wareHouseIn.TimeEnd;
            result.Pallet = wareHouseIn.Pallet;
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
                                                           Note= z.First().Note,
                                                           LotNo= z.First().LotNo,
                                                           TotalWeighScan= z.First().TotalWeighScan,
                                                           ProductDate= z.First().ProductDate,
                                                           ExpiryDate= z.First().ExpiryDate
                                                       }
                                                       )
                                                   });

            return result;
        }
        public async Task<IEnumerable<ReportReplaceInfoHTMLDTO>> GetDataWareHouseInReplaceThongTin(ReportReplaceWareHouseInParam param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_WareHouse_GetThongTinWareHouseInByIdReplace", param, commandType: System.Data.CommandType.StoredProcedure);
            var result = await rs.ReadAsync<ReportReplaceInfoHTMLDTO>().ConfigureAwait(false);
            return result;
        }
        public async Task<BieuMauInfoResponseViewModel> ExportWordThongTinAsync(ReportWareHouseInByIdReplaceViewModel request)
        {
            var param = _mapper.Map<WareHouseInByIdParam>(request);
            var queryResult = await GetDataWareHouseInByIdAsync(param).ConfigureAwait(false);
            HashSet<string> columKeywordQuaTrinhDaoTao = BaseReportCommon.GetPropertiesOfClass<WareHouseInDetailResponseDTO>();

            var dicMailMerge = await GetDataWareHouseInReplaceThongTin(new ReportReplaceWareHouseInParam { IdWareHouse = request.Id });
            Dictionary<string, string> replaceSameValues = dicMailMerge.ToDictionary(x => x.ObjKey, x => StringHelpers.Normalization(x.ObjValue));
            replaceSameValues.Add("NguoiXuatBan", "Hahaha");
            List<WordTemplateTable> wordTemplateTables = new List<WordTemplateTable>();
            wordTemplateTables.Add(new WordTemplateTable { ColumnKeyWord = columKeywordQuaTrinhDaoTao, DataTable = queryResult?.WareHouseInDetailResponseDTOs.OfType<object>().ToList(), Prefix = "#" });
   
            var bieuMau = await _sysBieuMauQueries.GetBieuMauByFilter(new SYSBieuMauFilterParam { MaBieuMau = ReportConstants.WareHouseIn_WHI001 });
            //ValidateBieuMau(bieuMau);

            MemoryStream outputStream;
            var isExportPDF = bieuMau.IsExportPDF;
            if (isExportPDF)
            {
                outputStream = _exportService.ExportPdfFromWord(bieuMau.NoiDung, replaceSameValues, wordTemplateTables, null, null);
            }
            else
            {
                outputStream = _exportService.ExportWordData(bieuMau.NoiDung, replaceSameValues, wordTemplateTables, null, null);
            }

            BieuMauInfoResponseViewModel bieuMauResponse = new BieuMauInfoResponseViewModel();
            bieuMauResponse.OutputStream = outputStream;
            bieuMauResponse.ContentType = isExportPDF ? ReportConstant.ContentTypeForPDF : _exportService.GetContentType(bieuMau.LoaiFile);
            bieuMauResponse.TenBieuMau = bieuMau.TenBieuMau + _exportService.GetExtensionFile(bieuMauResponse.ContentType);


            return bieuMauResponse;
        }
        public async Task<BieuMauInfoResponseViewModel> ExportExcelWareHouseInAsync(ReportWareHouseInByIdReplaceViewModel request)
        {
            var param = _mapper.Map<WareHouseInByIdParam>(request);
            var queryResult = await GetDataWareHouseInByIdAsync(param).ConfigureAwait(false);
            var dicMailMerge = await GetDataWareHouseInReplaceThongTin(new ReportReplaceWareHouseInParam { IdWareHouse = request.Id });
            Dictionary<string, string> replaceSameValues = dicMailMerge.ToDictionary(x => x.ObjKey, x => StringHelpers.Normalization(x.ObjValue));
            replaceSameValues.Add("NguoiXuatBan", "Hahaha");
            var bieuMau = await _sysBieuMauQueries.GetBieuMauByFilter(new SYSBieuMauFilterParam { MaBieuMau = ReportConstants.WareHouseIn_WHI002 });
            //ValidateBieuMau(bieuMau);

            MemoryStream outputStream;
            if (bieuMau.IsExportPDF)
            {
                outputStream = _exportService.ExportPdfFromExcel(queryResult.WareHouseInDetailResponseDTOs,
                    bieuMau.MaBieuMau, bieuMau.NoiDung, bieuMau.TenBieuMau, replaceSameValues);
            }
            else
            {
                outputStream = _exportService.ExportExcelData(queryResult.WareHouseInDetailResponseDTOs,
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