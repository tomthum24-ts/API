using API.APPLICATION;
using API.APPLICATION.Commands.RolePermission.Credential;
using API.APPLICATION.Parameters.Permission;
using API.APPLICATION.ViewModels.Permission;
using API.DOMAIN;
using API.DOMAIN.DTOs.Permission;
using AutoMapper;

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
            CreateMap<AllPermissionDTO, AllCredentialResponseViewModel>();
            CreateMap<PermissionByIdRequestViewModel, PermissionByIdFilterParam>();
            CreateMap<PermissionByIdDTO, PermissionByIdResponseViewModel>();
        }
    }
}