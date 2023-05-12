
using API.APPLICATION.Commands.Location.District;
using API.APPLICATION.Parameters.Location;
using API.APPLICATION.ViewModels.Location;
using API.DOMAIN;
using AutoMapper;

namespace API.Mapper
{
    public class DistrictProfile : Profile
    {
        public DistrictProfile()
        {
            CreateMap<District, CreateDistrictCommandResponse>();
            CreateMap<District, DeleteDistrictCommandResponse>();
            CreateMap<District, UpdateDistrictCommandResponse>();
            CreateMap<DistrictRequestViewModel, DistrictFilterParam>();
        }

    }
}
