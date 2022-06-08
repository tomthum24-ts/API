using API.APPLICATION.Commands.User;
using API.APPLICATION.Parameters.User;
using API.APPLICATION.ViewModels.User;
using API.HRM.DOMAIN;
using API.HRM.DOMAIN.DTOs.User;
using API.Models;
using AutoMapper;

namespace API.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //CreateMap<UserViewModel, UserDTO>();
            CreateMap<CreateUserCommand, CreateUserCommandResponse> ();
            CreateMap<User, CreateUserCommandResponse>();
            CreateMap<UpdateUserCommand, UpdateUserCommandResponse>();
            CreateMap<UserRequestViewModel, UserFilterParam>();
        }
    }
}
