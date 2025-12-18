using API.APPLICATION.Commands.JobApplys;
using API.APPLICATION.Commands.Jobs;
using API.DOMAIN;
using AutoMapper;

namespace API.Mapper
{
    public class jobApplysProfile: Profile
    {
        public jobApplysProfile()
        {
            CreateMap<JobApplys, CreateJobApplysCommandResponse>();
            CreateMap<JobApplys, DeleteJobApplysCommandResponse>();
            CreateMap<JobApplys, UpdateJobApplysCommandResponse>();
            //CreateMap<JobsRequestViewModel, JobsFilterParam>();

            //CreateMap<JobsDTO, JobsResponseViewModel>();
            //CreateMap<JobsByIdViewModel, JobsByIdParam>();
            //CreateMap<JobsByIdDTO, UserResponseByIdModel>();
        }
    }
}
