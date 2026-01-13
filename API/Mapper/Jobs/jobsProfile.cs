using API.APPLICATION.Commands.Jobs;
using API.APPLICATION.Parameters.Jobs;
using API.APPLICATION.ViewModels.Jobs;
using API.DOMAIN;
using API.DOMAIN.DTOs.Jobs;
using AutoMapper;

namespace API.Mapper
{
    public class jobsProfile : Profile
    {
        public jobsProfile()
        {
            CreateMap<Jobs, CreateJobsCommandResponse>();
            CreateMap<Jobs, DeleteJobsCommandResponse>();
            CreateMap<Jobs, UpdateJobsCommandResponse>();
            CreateMap<JobsRequestViewModel, JobsFilterParam>();

            CreateMap<JobsDTO, JobsResponseViewModel>();
            CreateMap<JobsByIdViewModel, JobsByIdParam>();
            CreateMap<JobsByIdDTO, JobsResponseViewModel>();
        }
    }
}
