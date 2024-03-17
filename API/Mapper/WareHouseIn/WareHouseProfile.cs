using API.APPLICATION.Commands.WareHouseIn;
using API.APPLICATION.Parameters.WareHouseIn;
using API.APPLICATION.ViewModels.User;
using API.APPLICATION.ViewModels.WareHouseIn;
using API.APPLICATION.ViewModels.WareHouseInDetail;
using API.DOMAIN.DTOs.WareHouseIn;
using AutoMapper;

namespace API.Mapper.WareHouseIn
{
    public class WareHouseProfile : Profile
    {
        public WareHouseProfile()
        {
            CreateMap<CreateWareHouseInCommand, CreateWareHouseInCommandResponse>();

            CreateMap<WareHouseRequestViewModel, WareHouseInFilterParam>();
            CreateMap<WareHouseInDTO,  WareHouseInResponseViewModel> ();
            CreateMap<WareHouseInByIdViewModel, WareHouseInByIdParam> ();
            CreateMap<WareHouseInResponseViewModel, WareHouseInByIdDTO> ();
            //CreateMap<WareHouseInDetailResponseViewModel, WareHouseInDetailResponseViewModel> ();
            
        }
    }
}