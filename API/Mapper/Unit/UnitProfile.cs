using API.APPLICATION;
using API.APPLICATION.Commands.WareHouseIn;
using API.APPLICATION.Parameters.WareHouseIn;
using API.APPLICATION.ViewModels.Unit;
using API.APPLICATION.ViewModels.WareHouseIn;
using API.DOMAIN.DTOs.Unit;
using API.DOMAIN.DTOs.WareHouseIn;
using AutoMapper;

namespace API.Mapper.Unit
{
    public class UnitProfile :  Profile
    {
        public UnitProfile()
        {
   
            CreateMap<UnitRequestViewModel,  DanhMucFilterParam> ();
            CreateMap<UnitDTO, UnitResponseViewModel > ();

        }
    }
}
