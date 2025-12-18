using API.APPLICATION.Commands.Jobs;
using API.DOMAIN;
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
            //CreateMap<JobsRequestViewModel, JobsFilterParam>();

            //CreateMap<JobsDTO, JobsResponseViewModel>();
            //CreateMap<JobsByIdViewModel, JobsByIdParam>();
            //CreateMap<JobsByIdDTO, UserResponseByIdModel>();
        }
    }
}
