using API.APPLICATION.Commands.JobApplys;
using API.APPLICATION.Commands.Jobs;
using API.APPLICATION.Parameters.JobApplys;
using API.APPLICATION.ViewModels.JobApplys;
using API.DOMAIN;
using API.DOMAIN.DTOs;
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
            CreateMap<JobApplysRequestViewModel, JobApplysFilterParam>();
            CreateMap<JobApplysByIdViewModel, JobApplysByIdParam>();

            CreateMap<JobApplysDTO, JobApplysResponseViewModel>();
        }
    }
}
