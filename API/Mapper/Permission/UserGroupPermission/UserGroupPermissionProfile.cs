using API.APPLICATION;
using API.APPLICATION.Commands.RolePermission.Role;
using API.APPLICATION.ViewModels.Permission;
using API.DOMAIN.DomainObjects.Permission;
using API.DOMAIN.DTOs.Permission;
using AutoMapper;

namespace API.Mapper.Permission.UserGroupPermission
{
    public class UserGroupPermissionProfile : Profile
    {
        public UserGroupPermissionProfile()
        {
            CreateMap<UserGroupPermissionDTO, UserGroupPermissionResponseViewModel>();
            CreateMap<UserGroupPermissionRequestViewModel, DanhMucFilterParam> ();
            CreateMap<UserGroupPermissions, CreateRolePermissionCommandResponse>();
            CreateMap<UserGroupPermissions, DeleteRoleCommandResponse>();
            CreateMap<UserGroupPermissions, UpdateRoleCommandResponse>();
            CreateMap<RolePermissionDTO, RolePermissionResponseViewModel>();

        }
    }
}