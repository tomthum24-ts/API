using API.APPLICATION.Parameters;
using API.DOMAIN.DTOs;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.Response;
using Dapper;
using System;
using System.Data;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries
{
    public interface ISYSBieuMauQueries
    {
        public Task<SYSBieuMauDTO> GetBieuMauByFilter(SYSBieuMauFilterParam param);

        public Task<PagingItems<SYSBieuMauDTO>> GetBieuMauAsync(SysBieuMauPagingFilterParam param);
    }

    public class SYSBieuMauQueries : ISYSBieuMauQueries
    {
        public readonly DapperContext _context;
        private readonly IUserSessionInfo _userSessionInfo;

        public SYSBieuMauQueries(DapperContext connectionFactory, IUserSessionInfo UserSessionInfo)
        {
            _context = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _userSessionInfo = UserSessionInfo;
        }

        public async Task<SYSBieuMauDTO> GetBieuMauByFilter(SYSBieuMauFilterParam param)
        {
            //if (_userSessionInfo.ID != null)
            //    param.IdNhanSuLogin = _userSessionInfo.ID.GetValueOrDefault();
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_SYSBieuMau_SelectByFilter", param, commandType: CommandType.StoredProcedure);
            var result = await rs.ReadFirstOrDefaultAsync<SYSBieuMauDTO>().ConfigureAwait(false);
            return result;
        }

        public async Task<PagingItems<SYSBieuMauDTO>> GetBieuMauAsync(SysBieuMauPagingFilterParam param)
        {
            var result = new PagingItems<SYSBieuMauDTO>
            {
                PagingInfo = new PagingInfoDto
                {
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize
                }
            };

            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_SYSBieuMau_SelectByFilter", param, commandType: CommandType.StoredProcedure);
            result.Items = await rs.ReadAsync<SYSBieuMauDTO>().ConfigureAwait(false);
            result.PagingInfo.TotalItems = await rs.ReadSingleAsync<int>().ConfigureAwait(false);
            return result;
        }
    }
}