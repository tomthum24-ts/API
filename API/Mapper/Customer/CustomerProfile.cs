using API.APPLICATION.Commands.Customer;
using API.APPLICATION.Parameters.Customer;
using API.APPLICATION.ViewModels.Customer;
using API.APPLICATION.ViewModels.User;
using API.DOMAIN;
using API.DOMAIN.DTOs.Customer;
using AutoMapper;

namespace API.Mapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CreateCustomerCommandResponse>();
            CreateMap<Customer, DeleteCustomerCommandResponse>();
            CreateMap<Customer, UpdateCustomerCommandResponse>();
            CreateMap<CustomerRequestViewModel, CustomerFilterParam>();

            CreateMap<CustomerDTO, CustomerResponseViewModel>();
            CreateMap<CustomerByIdViewModel,  CustomerByIdParam> ();
            CreateMap<CustomerByIdDTO, UserResponseByIdModel>();
        }
    }
}