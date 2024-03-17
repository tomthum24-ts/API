using API.DOMAIN.DTOs.Unit;
using API.INFRASTRUCTURE.DataConnect;

namespace API.APPLICATION.Queries.Unit
{
    public interface IUnitServices : IDanhMucQueries<UnitDTO>
    {
    }

    public class UnitServices : DanhMucQueries<UnitDTO>, IUnitServices
    {
        public UnitServices(DapperContext connectionFactory) : base(connectionFactory)
        {
        }
    }
}