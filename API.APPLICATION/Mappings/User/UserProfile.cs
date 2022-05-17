using API.APPLICATION.Commands.User;
using API.HRM.DOMAIN;
using API.HRM.DOMAIN.DTOs.User;
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
        }
    }
}
