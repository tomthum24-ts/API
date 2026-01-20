using API.APPLICATION;
using API.APPLICATION.Commands.Project;
using API.APPLICATION.Commands.JobsCategory.CreateJobsCategory;
using API.APPLICATION.Commands.JobsCategory.UpdateJobsCategory;
using API.APPLICATION.ViewModels.JobsCategory;
using API.DOMAIN;
using API.DOMAIN.DTOs;
using AutoMapper;

namespace API.Mapper
{
    public class JobsCategoryProfile : Profile
    {
        public JobsCategoryProfile()
        {
            CreateMap< JobsCategoryRequestViewModel, DanhMucFilterParam>();
            CreateMap<JobsCategoryDTO, JobsCategoryModel>();
            CreateMap<JobsCategory, CreateJobsCategoryCommandResponse>();
            CreateMap<JobsCategory, UpdateJobsCategoryCommandResponse>();
        }
    }
}
