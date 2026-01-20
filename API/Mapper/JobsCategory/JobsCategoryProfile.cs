using API.APPLICATION;
using API.APPLICATION.Commands.Project;
using API.APPLICATION.ViewModels.JobsCategory;
using API.DOMAIN;
using API.DOMAIN.DTOs;
using AutoMapper;

namespace API.Mapper.JobsCategory
{
    public class JobsCategoryProfile : Profile
    {
        public JobsCategoryProfile()
        {
            CreateMap< JobsCategoryRequestViewModel, DanhMucFilterParam>();
            CreateMap<JobsCategoryDTO, JobsCategoryModel>();
        }
    }
}
