
using API.APPLICATION.Commands.Location.District;
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
        }

    }
}
