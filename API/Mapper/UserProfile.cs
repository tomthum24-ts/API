using API.Commands;
using API.DTOs;
using API.Models;
using AutoMapper;

namespace API.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel, UserDTO>();
            CreateMap< UserViewModel, CreateUserCommandResponse> ();
        }
    }
}
