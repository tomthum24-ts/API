using API.APPLICATION.Commands.RolePermission.Credential;
using AutoMapper;
using API.DOMAIN;
using API.DOMAIN.DTOs.Permission;
using API.APPLICATION.ViewModels.Permission;
using API.APPLICATION;

namespace API.Mapper
{
    public class CredentialProfile : Profile
    {
        public CredentialProfile()
        {
            CreateMap<PM_Credential, CreateCredentialCommandResponse>();
            CreateMap<PM_Credential, DeleteCredentialCommandResponse>();
            CreateMap<PM_Credential, UpdateCredentialCommandResponse>();
            CreateMap<CredentialDTO, CredentialResponseViewModel>();
            CreateMap<CredentialRequestViewModel, DanhMucFilterParam>();
        }
    }
}
