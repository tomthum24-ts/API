using API.APPLICATION.Parameters.Jobs;
using API.APPLICATION.Queries.Jobs;
using API.DOMAIN.DTOs.Jobs;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.Jobs
{
    public interface IJobsServices
    {
        Task<PagingItems<JobsDTO>> GetJobsPagingAsync(JobsFilterParam param);
        Task<JobsByIdDTO> GetInfoUserByIdAsync(JobsByIdParam param);
    }
    public class JobsServices : IJobsServices
    {
        public readonly DapperContext _context;

        public JobsServices(DapperContext context)
        {
            _context = context;
        }
        public async Task<PagingItems<JobsDTO>> GetJobsPagingAsync(JobsFilterParam param)
        {
            var result = new PagingItems<JobsDTO>
            {
                PagingInfo = new PagingInfoDto
                {
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize,
                }
            };
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DM_LC_GetListJobs_SelectWithPaging", param, commandType: CommandType.StoredProcedure);
            result.Items = await rs.ReadAsync<JobsDTO>().ConfigureAwait(false);
            result.PagingInfo.TotalItems = await rs.ReadSingleAsync<int>().ConfigureAwait(false);
            return result;
        }
        public async Task<JobsByIdDTO> GetInfoUserByIdAsync(JobsByIdParam param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DA_GetInfoJobById", param, commandType: CommandType.StoredProcedure);
            var result = await rs.ReadFirstOrDefaultAsync<JobsByIdDTO>().ConfigureAwait(false);
            return result;
        }
    }
}
