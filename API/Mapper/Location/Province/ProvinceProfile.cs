using API.APPLICATION.Commands.Location.Province;
using API.APPLICATION.Parameters.Location;
using API.APPLICATION.ViewModels.Location;
using API.DOMAIN;
using AutoMapper;

namespace API.Mapper
{
    public class ProvinceProfile : Profile
    {
        public ProvinceProfile()
        {
            CreateMap<Province, CreateProvinceCommandResponse>();
            CreateMap<Province, DeleteProvinceCommandResponse>();
            CreateMap<Province, UpdateProvinceCommandResponse>();
            CreateMap<ProvinceRequestViewModel, ProvinceFilterParam>();
        }

    }
}
