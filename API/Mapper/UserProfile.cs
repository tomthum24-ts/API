using API.DTOs;
using API.Models;
using AutoMapper;

namespace API.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, UserReadDTO>();
            CreateMap<UserCreateDTO, UserDTO>();
        }
    }
}
