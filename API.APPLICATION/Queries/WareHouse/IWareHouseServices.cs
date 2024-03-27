using API.DOMAIN.DTOs;
using API.INFRASTRUCTURE.DataConnect;

namespace API.APPLICATION.Queries.WareHouse
{
    public interface IWareHouseServices : IDanhMucQueries<WareHouseDTO>
    {
    }

    public class WareHouseServices : DanhMucQueries<WareHouseDTO>, IWareHouseServices
    {
        public WareHouseServices(DapperContext connectionFactory) : base(connectionFactory)
        {
        }
    }
}