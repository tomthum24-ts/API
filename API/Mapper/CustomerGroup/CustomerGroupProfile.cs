using API.APPLICATION;
using API.APPLICATION.ViewModels.CustomerGroup;
using API.DOMAIN.DTOs.CustomerGroup;
using AutoMapper;

namespace API.Mapper.CustomerGroup
{
    public class CustomerGroupProfile : Profile
    {
        public CustomerGroupProfile()
        {
            CreateMap<CustomerGroupRequestViewModel, DanhMucFilterParam>();
            CreateMap<CustomerGroupDTO, CustomerGroupResponseViewModel>();
        }
    }
}