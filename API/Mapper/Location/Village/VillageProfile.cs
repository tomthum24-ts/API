using API.APPLICATION.Commands.Location.Village;
using API.DOMAIN;
using AutoMapper;

namespace API.Mapper
{
    public class VillageProfile : Profile
    {
        public VillageProfile()
        {
            CreateMap<Village, CreateVillageCommandResponse>();
            CreateMap<Village, DeleteVillageCommandResponse>();
            CreateMap<Village, UpdateVillageCommandResponse>();
        }

    }
}
