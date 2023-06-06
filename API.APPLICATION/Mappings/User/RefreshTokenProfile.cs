using API.APPLICATION.Commands.RefreshTooken;
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
            CreateMap<CreateRefreshTookenCommand, CreateRefreshTookenCommandResponse>();
            CreateMap<RefreshToken, CreateUserCommandResponse>();
            CreateMap<Tokens, UpdateRefreshTookenCommandResponse>().ForMember(x => x.AccessToken,options => options.MapFrom(source => source.Token)); ;
            //CreateMap<UserDTO, UserResponseViewModel>();
        }
        
    }
}
