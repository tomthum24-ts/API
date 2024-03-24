using API.APPLICATION.Commands.WareHouseIn;
using API.APPLICATION.Parameters.WareHouseIn;
using API.APPLICATION.ViewModels.ByIdViewModel;
using API.APPLICATION.ViewModels.WareHouseIn;
using API.DOMAIN.DTOs.WareHouseIn;
using AutoMapper;

namespace API.Mapper.WareHouseIn
{
    public class WareHouseInProfile : Profile
    {
        public WareHouseInProfile()
        {
            CreateMap<CreateWareHouseInCommand, CreateWareHouseInCommandResponse>();

            CreateMap<WareHouseInRequestViewModel, WareHouseInFilterParam>();
            CreateMap<WareHouseInDTO, WareHouseInResponseViewModel>();
            CreateMap<WareHouseInByIdViewModel, WareHouseInByIdParam>();
            CreateMap<WareHouseInResponseViewModel, WareHouseInByIdDTO>();
            CreateMap< UpdateWareHouseInCommand, UpdateWareHouseInCommandResponse> ();
            CreateMap< ReportWareHouseInByIdReplaceViewModel, WareHouseInByIdParam> ();
        }
    }
}