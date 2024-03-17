using API.DOMAIN.DTOs.Vehicle;
using API.INFRASTRUCTURE.DataConnect;

namespace API.APPLICATION.Queries.Vehicle
{
    public interface IVehicleServices : IDanhMucQueries<VehicleDTO>
    {
    }

    public class VehicleServices : DanhMucQueries<VehicleDTO>, IVehicleServices
    {
        public VehicleServices(DapperContext connectionFactory) : base(connectionFactory)
        {
        }
    }
}