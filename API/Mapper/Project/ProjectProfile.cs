using API.APPLICATION.Commands.Project;
using API.APPLICATION.Commands.User;
using API.DOMAIN;
using AutoMapper;

namespace API.Mapper
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, CreateProjectCommandResponse>();
            CreateMap<Project, DeleteProjectCommandResponse>();
            CreateMap<Project, UpdateProjectCommandResponse>();
        }

    }
}
