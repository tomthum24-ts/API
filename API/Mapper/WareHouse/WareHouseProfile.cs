using API.APPLICATION.ViewModels.WareHouse;
using API.APPLICATION;
using AutoMapper;
using API.DOMAIN.DTOs;

namespace API.Mapper.WareHouse
{

    public class WareHouseProfile : Profile
    {
        public WareHouseProfile()
        {
            CreateMap<WareHouseRequestViewModel, DanhMucFilterParam>();
            CreateMap<WareHouseDTO, WareHouseResponseViewModel>();
        }
    }
}