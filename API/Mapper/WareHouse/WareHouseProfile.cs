using API.APPLICATION.ViewModels.WareHouse;
using API.APPLICATION;
using AutoMapper;
using API.DOMAIN.DTOs;
using API.DOMAIN.DTOs.WareHouse;
using API.APPLICATION.Parameters.WareHouse;

namespace API.Mapper.WareHouse
{

    public class WareHouseProfile : Profile
    {
        public WareHouseProfile()
        {
            CreateMap<WareHouseRequestViewModel, DanhMucFilterParam>();
            CreateMap<WareHouseDTO, WareHouseResponseViewModel>(); 
            CreateMap<WareHouseAllDTO, WareHouseAllResponseViewModel>();
            CreateMap<WareHouseAllRequestViewModel, WareHouseAllFilterParam>();
        }
    }
}