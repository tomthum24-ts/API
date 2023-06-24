using API.APPLICATION.Commands.RefreshToken;
using API.APPLICATION.Commands.User;
using API.APPLICATION.ViewModels.User;
using API.DOMAIN;
using API.DOMAIN.DTOs.User;
using AutoMapper;
namespace API.APPLICATION.Mappings.User
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<CreateRefreshTokenCommand, CreateRefreshTokenCommandResponse>();
            CreateMap<UserRefreshToken, CreateUserCommandResponse>();
            CreateMap<Tokens, UpdateRefreshTokenCommandResponse>().ForMember(x => x.AccessToken,options => options.MapFrom(source => source.Token)); ;
            //CreateMap<UserDTO, UserResponseViewModel>();
        }
        
    }
}
