using API.APPLICATION.Parameters.Location;
using API.APPLICATION.ViewModels.ByIdViewModel;
using API.DOMAIN.DTOs.Location;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.Response;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.Location
{
    public interface IDistrictServices
    {
        Task<PagingItems<DistrictDTO>> GetDistrictAsync(DistrictFilterParam param);
        Task<IEnumerable<ResponseByIdViewModel>> GetInfoByIdAsync(RequestByIdViewModel param);
    }
    public class DistrictServices : IDistrictServices
    {
        public readonly DapperContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributedCache _cache;
        public DistrictServices(DapperContext context, IHttpContextAccessor httpContextAccessor, IDistributedCache cache)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }
        public async Task<PagingItems<DistrictDTO>> GetDistrictAsync(DistrictFilterParam param)
        {
            var data = GetCurrentAsync();
             await DeactivateAsync(data);
            var result = new PagingItems<DistrictDTO>
            {
                PagingInfo = new PagingInfoDto
                {
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize,
                }
            };
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DM_LC_GetListDistrict_SelectWithPaging", param, commandType: CommandType.StoredProcedure);
            result.Items = await rs.ReadAsync<DistrictDTO>().ConfigureAwait(false);
            result.PagingInfo.TotalItems = await rs.ReadSingleAsync<int>().ConfigureAwait(false);
            return result;
        }
        public async Task<IEnumerable<ResponseByIdViewModel>> GetInfoByIdAsync(RequestByIdViewModel param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_LC_GetDistrictByIdReplace", param, commandType: System.Data.CommandType.StoredProcedure);
            var result = await rs.ReadAsync<ResponseByIdViewModel>().ConfigureAwait(false);
            return result; ;
        }
        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }
        public async Task DeactivateAsync(string token)
           => await _cache.SetStringAsync(token,
               " ", new DistributedCacheEntryOptions
               {
                   AbsoluteExpirationRelativeToNow =
                       TimeSpan.FromMinutes(0.1)
               });


    }
}
