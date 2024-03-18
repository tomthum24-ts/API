using API.DOMAIN.DTOs.CustomerGroup;
using API.INFRASTRUCTURE.DataConnect;

namespace API.APPLICATION.Queries.CustomerGroup
{
    public interface ICustomerGroupServices : IDanhMucQueries<CustomerGroupDTO>
    {
    }

    public class CustomerGroupServices : DanhMucQueries<CustomerGroupDTO>, ICustomerGroupServices
    {
        public CustomerGroupServices(DapperContext connectionFactory) : base(connectionFactory)
        {
        }
    }
}