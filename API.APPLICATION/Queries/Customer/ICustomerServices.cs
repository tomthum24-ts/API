using API.APPLICATION.Parameters.Customer;
using API.APPLICATION.Parameters.User;
using API.DOMAIN.DTOs.Customer;
using API.DOMAIN.DTOs.User;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.Response;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.Customer
{
    public interface ICustomerServices
    {
        Task<PagingItems<CustomerDTO>> GetCustomerPagingAsync(CustomerFilterParam param);
        Task<CustomerByIdDTO> GetInfoUserByIdAsync(CustomerByIdParam param);
    }
    public class CustomerServices : ICustomerServices
    {
        public readonly DapperContext _context;

        public CustomerServices(DapperContext context)
        {
            _context = context;
        }

        public async Task<PagingItems<CustomerDTO>> GetCustomerPagingAsync(CustomerFilterParam param)
        {
            var result = new PagingItems<CustomerDTO>
            {
                PagingInfo = new PagingInfoDto
                {
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize,
                }
            };
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DM_LC_GetListCustomer_SelectWithPaging", param, commandType: CommandType.StoredProcedure);
            result.Items = await rs.ReadAsync<CustomerDTO>().ConfigureAwait(false);
            result.PagingInfo.TotalItems = await rs.ReadSingleAsync<int>().ConfigureAwait(false);
            return result;
        }
        public async Task<CustomerByIdDTO> GetInfoUserByIdAsync(CustomerByIdParam param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_DA_GetInfoCustomerById", param, commandType: CommandType.StoredProcedure);
            var result = await rs.ReadFirstOrDefaultAsync<CustomerByIdDTO>().ConfigureAwait(false);
            return result;
        }
    }
}
