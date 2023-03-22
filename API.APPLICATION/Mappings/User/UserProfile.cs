using API.APPLICATION.Commands.User;
using API.APPLICATION.ViewModels.User;
using API.DOMAIN;
using API.DOMAIN.DTOs.User;
using AutoMapper;


namespace API.APPLICATION
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //CreateMap<UserViewModel, UserDTO>();
            CreateMap<CreateUserCommand, CreateUserCommandResponse>();
            CreateMap<User, CreateUserCommandResponse>();
            CreateMap<UpdateUserCommand, UpdateUserCommandResponse>();
            CreateMap< UserDTO, UserResponseViewModel>();
        }
    }
}
