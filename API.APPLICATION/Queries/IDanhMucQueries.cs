
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.MethodResult;
using BaseCommon.Common.Response;
using BaseCommon.ExceptionsModels;
using BaseCommon.Model;
using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries
{
    public interface IDanhMucQueries<T> where T : class
    {
        Task<PagingItems<T>> GetDanhMucByListIdAsync(DanhMucFilterParam danhMucFilterParam);
        Task<PagingItems<T>> GetDanhMucByIdAsync(int findId = 0, string tableName = "", int pageSize = 0, int pageNumber = 0);
    }
    public class DanhMucQueries<T> : IDanhMucQueries<T> where T : class
    {
        public readonly DapperContext _context;

        public DanhMucQueries(DapperContext context)
        {
            _context = context;
        }

        public async Task<PagingItems<T>> GetDanhMucByIdAsync(int findId = 0, string tableName = "", int pageSize = 0, int pageNumber = 0)
        {
            string columnName = GetColumnTableName();
            var result = new PagingItems<T>
            {
                PagingInfo = new PagingInfoDto
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }
            };
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DanhMuc_Base_SelectWithPaging", new { FindId = findId, PageSize = pageSize, PageNumber = pageNumber, TableName = tableName, ColumName = columnName }, commandType: CommandType.StoredProcedure);
            result.Items = await rs.ReadAsync<T>().ConfigureAwait(false);
            result.PagingInfo.TotalItems = await rs.ReadSingleAsync<int>().ConfigureAwait(false);
            return result;
        }
        public async Task<PagingItems<T>> GetDanhMucByListIdAsync(DanhMucFilterParam danhMucFilterParam)
        {
            if (SecurityHelper.CheckForSQLInjection(danhMucFilterParam.KeyWord))
            {
                var errorResult = new ErrorResult
                {
                    ErrorCode = CommonErrors.InvalidFormat,
                    ErrorMessage = ErrorHelpers.GetCommonErrorMessage(CommonErrors.InvalidFormat)
                };

                throw new ServiceException(errorResult);
            }

            danhMucFilterParam.ColumName = GetColumnTableName();

            var result = new PagingItems<T>
            {
                PagingInfo = new PagingInfoDto
                {
                    PageNumber = danhMucFilterParam.PageNumber,
                    PageSize = danhMucFilterParam.PageSize
                }
            };

            var conn = _context.CreateConnection();

            using var rs = await conn.QueryMultipleAsync("SP_DanhMuc_Base_SelectWithPaging", danhMucFilterParam, commandType: CommandType.StoredProcedure);

            result.Items = await rs.ReadAsync<T>().ConfigureAwait(false);

            result.PagingInfo.TotalItems = await rs.ReadSingleAsync<int>().ConfigureAwait(false);

            return result;
        }
        public string GetColumnTableName()
        {
            var properties = typeof(T).GetProperties();
            return string.Join(",", properties.Select(x => x.Name));
        }

    }

}
