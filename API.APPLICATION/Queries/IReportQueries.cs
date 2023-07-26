using API.APPLICATION.Parameters;
using API.DOMAIN;
using API.INFRASTRUCTURE.DataConnect;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries
{
    public interface IReportQueries
    {
        public Task<IEnumerable<ReplaceInfoHTMLDTO>> GetInformationReportReplaceInfo(ReportReplaceInfoFilterParam param);
    }

    public class ReportQueries : IReportQueries
    {
        public readonly DapperContext _context;

        public ReportQueries(DapperContext connectionFactory)
        {
            _context = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public async Task<IEnumerable<ReplaceInfoHTMLDTO>> GetInformationReportReplaceInfo(ReportReplaceInfoFilterParam param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_NS_GetThongTinReplaceInfo", param, commandType: System.Data.CommandType.StoredProcedure);
            var result = await rs.ReadAsync<ReplaceInfoHTMLDTO>().ConfigureAwait(false);
            return result;
        }
    }
}