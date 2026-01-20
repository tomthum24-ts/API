using API.APPLICATION.Parameters.JobApplys;
using API.DOMAIN.DTOs;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.Response;
using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.JobApplys
{
    public interface IJobApplysServices
    {
        Task<PagingItems<JobApplysDTO>> GetJobApplysPagingAsync(JobApplysFilterParam param);
        Task<JobApplysByIdDTO> GetInfoJobApplysByIdAsync(JobApplysByIdParam param);
    }

    public class JobApplysServices : IJobApplysServices
    {
        public readonly DapperContext _context;

        public JobApplysServices(DapperContext context)
        {
            _context = context;
        }

        public async Task<PagingItems<JobApplysDTO>> GetJobApplysPagingAsync(JobApplysFilterParam param)
        {
            var result = new PagingItems<JobApplysDTO>
            {
                PagingInfo = new PagingInfoDto
                {
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize,
                }
            };
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DM_LC_GetListJobApplys_SelectWithPaging", param, commandType: CommandType.StoredProcedure);
            result.Items = await rs.ReadAsync<JobApplysDTO>().ConfigureAwait(false);
            result.PagingInfo.TotalItems = await rs.ReadSingleAsync<int>().ConfigureAwait(false);
            return result;
        }

        public async Task<JobApplysByIdDTO> GetInfoJobApplysByIdAsync(JobApplysByIdParam param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DA_GetInfoJobApplysById", param, commandType: CommandType.StoredProcedure);
            var result = await rs.ReadFirstOrDefaultAsync<JobApplysByIdDTO>().ConfigureAwait(false);
            return result;
        }
    }
}
