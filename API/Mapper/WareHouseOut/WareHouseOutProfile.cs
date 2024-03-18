using API.APPLICATION.Commands.WareHouseOut;
using API.APPLICATION.Parameters.WareHouseOut;
using API.APPLICATION.ViewModels.WareHouseOut;
using API.DOMAIN.DTOs.WareHouseOut;
using AutoMapper;

namespace API.Mapper.WareHouseOut
{
    public class WareHouseOutProfile : Profile
    {
        public WareHouseOutProfile()
        {
            CreateMap<CreateWareHouseOutCommand, CreateWareHouseOutCommandResponse>();

            CreateMap<WareHouseOutRequestViewModel, WareHouseOutFilterParam>();
            CreateMap<WareHouseOutDTO, WareHouseOutResponseViewModel>();
            CreateMap<WareHouseOutByIdViewModel, WareHouseOutByIdParam>();
            CreateMap<WareHouseOutResponseViewModel, WareHouseOutByIdDTO>();
            CreateMap< UpdateWareHouseOutCommand, UpdateWareHouseOutCommandResponse> ();
        }
    }
}