using API.APPLICATION.Parameters;
using API.APPLICATION.Parameters.User;
using API.APPLICATION.Queries;
using API.APPLICATION.ViewModels.BieuMau;
using API.APPLICATION.ViewModels.ByIdViewModel;
using API.DOMAIN;
using API.DOMAIN.DTOs;
using API.DOMAIN.DTOs.User;
using API.INFRASTRUCTURE.DataConnect;
using AutoMapper;
using BaseCommon.Common.Report;
using BaseCommon.Common.Report.Infrastructures;
using BaseCommon.Common.Report.Interfaces;
using BaseCommon.Common.Report.Models;
using BaseCommon.Common.Response;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE
{
    public interface IUserServices
    {
        Task<IEnumerable<UserDTO>> GetAllUser();

        Task<IEnumerable<ResponseByIdViewModel>> GetInfoUserByID(RequestByIdViewModel param);

        Task<PagingItems<UserDTO>> GetUserPagingAsync(UserFilterParam param);

        Task<BieuMauInfoResponseViewModel> ExportWordThongTinAsync(RequestByIdViewModel request);
    }

    public class UserServices : IUserServices
    {
        public readonly DapperContext _context;
        protected readonly ISYSBieuMauQueries _sysBieuMauQueries;
        private readonly IMapper _mapper;
        protected readonly IReportQueries _reportQueries;
        private readonly IExportService _exportService;

        public UserServices(DapperContext context, ISYSBieuMauQueries sysBieuMauQueries, IMapper mapper, IExportService exportService)
        {
            _context = context;
            _sysBieuMauQueries = sysBieuMauQueries;
            _mapper = mapper;
            _exportService = exportService;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUser()
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("GetAllUser", new { }, commandType: CommandType.StoredProcedure);
            var result = await rs.ReadAsync<UserDTO>().ConfigureAwait(false);
            return result;
        }

        public async Task<PagingItems<UserDTO>> GetUserPagingAsync(UserFilterParam param)
        {
            var result = new PagingItems<UserDTO>
            {
                PagingInfo = new PagingInfoDto
                {
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize,
                }
            };
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_User_GetListUser_SelectWithPaging", param, commandType: CommandType.StoredProcedure);
            result.Items = await rs.ReadAsync<UserDTO>().ConfigureAwait(false);
            result.PagingInfo.TotalItems = await rs.ReadSingleAsync<int>().ConfigureAwait(false);
            return result;
        }

        public async Task<IEnumerable<ResponseByIdViewModel>> GetInfoUserByID(RequestByIdViewModel param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_User_GetThongTinUserByIdReplace", param, commandType: System.Data.CommandType.StoredProcedure);
            var result = await rs.ReadAsync<ResponseByIdViewModel>().ConfigureAwait(false);
            return result;
        }

        public async Task<BieuMauInfoResponseViewModel> ExportWordThongTinAsync(RequestByIdViewModel request)
        {
            //var param = _mapper.Map<ThongTinLyLichNhanSuFilterParam>(request);
            var queryResult = await GetAllUser().ConfigureAwait(false);
            HashSet<string> columKeywordQuaTrinhDaoTao = BaseReportCommon.GetPropertiesOfClass<UserDTO>();
            Dictionary<string, string> replaceSameValues = new Dictionary<string, string>();
            replaceSameValues.Add("NguoiXuatBan","Hahaha");
            List<WordTemplateTable> wordTemplateTables = new List<WordTemplateTable>();
            wordTemplateTables.Add(new WordTemplateTable { ColumnKeyWord = columKeywordQuaTrinhDaoTao, DataTable = queryResult.OfType<object>().ToList(), Prefix = "#" });

            var bieuMau = await _sysBieuMauQueries.GetBieuMauByFilter(new SYSBieuMauFilterParam { MaBieuMau = ReportConstants.DanhSachNhanSu_NS10001 });
            //ValidateBieuMau(bieuMau);

            MemoryStream outputStream;
            var isExportPDF = bieuMau.IsExportPDF;
            if (isExportPDF)
            {
                outputStream = _exportService.ExportPdfFromWord(bieuMau.NoiDung, null, wordTemplateTables, null, null);
            }
            else
            {
                outputStream = _exportService.ExportWordData(bieuMau.NoiDung, replaceSameValues, wordTemplateTables, null, null);
            }

            BieuMauInfoResponseViewModel bieuMauResponse = new BieuMauInfoResponseViewModel();
            bieuMauResponse.OutputStream = outputStream;
            bieuMauResponse.ContentType = isExportPDF ? ReportConstant.ContentTypeForPDF : _exportService.GetContentType(bieuMau.LoaiFile);
            bieuMauResponse.TenBieuMau = bieuMau.TenBieuMau + _exportService.GetExtensionFile(bieuMauResponse.ContentType);
            //await _bieuMauLogger.LogAsync(bieuMauResponse.TenBieuMau, HRMExcelConstants.LyLichKhoaHoc_V10028, bieuMauResponse.ContentType, bieuMauResponse.OutputStream.ToArray());
            return bieuMauResponse;
        }

    }
}