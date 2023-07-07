using API.APPLICATION.Commands.RolePermission.Credential;
using AutoMapper;
using API.DOMAIN;
namespace API.Mapper
{
    public class CredentialProfile : Profile
    {
        public CredentialProfile()
        {
            CreateMap<PM_Credential, CreateCredentialCommandResponse>();
            //CreateMap<Credential, ChangeCredentialCommandResponse>();
            CreateMap<PM_Credential, DeleteCredentialCommandResponse>();
            CreateMap<PM_Credential, UpdateCredentialCommandResponse>();
        }
    }
}
