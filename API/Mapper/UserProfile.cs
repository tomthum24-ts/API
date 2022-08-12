using API.APPLICATION.Commands.Login;
using API.APPLICATION.Commands.User;
using API.APPLICATION.Parameters.User;
using API.APPLICATION.ViewModels.User;
using API.HRM.DOMAIN;
using API.HRM.DOMAIN.DTOs.User;
using API.Models;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace API.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //CreateMap<CreateUserCommand, CreateUserCommandResponse> ();
            #region User
            CreateMap<User, CreateUserCommandResponse>();
            CreateMap<User, ChangePasswordCommandResponse>();
            CreateMap<User, DeleteUserCommandResponse>();
            CreateMap<User, UpdateUserCommandResponse>();
            CreateMap<UserRequestViewModel, UserFilterParam>();
            CreateMap<SecurityToken, Tokens>().ForMember(x=>x.Token,o=>o.MapFrom(y=>y.SigningKey));
            CreateMap<Tokens, LoginCommandResponse>().ForMember(x=>x.AccessToken,o=>o.MapFrom(y=>y.Token));

            #endregion
            //CreateMap<UpdateUserCommand, UpdateUserCommandResponse>();



        }
    }
}
