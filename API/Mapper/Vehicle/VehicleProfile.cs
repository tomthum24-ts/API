using API.APPLICATION;
using API.APPLICATION.ViewModels.Vehicle;
using API.DOMAIN.DTOs.Vehicle;
using AutoMapper;

namespace API.Mapper.Vehicle
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleRequestViewModel, DanhMucFilterParam>();
            CreateMap<VehicleDTO, VehicleResponseViewModel>();
        }
    }
}