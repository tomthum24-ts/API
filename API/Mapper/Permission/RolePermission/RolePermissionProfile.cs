using API.APPLICATION;
using API.APPLICATION.Commands.RolePermission.Role;
using API.APPLICATION.ViewModels.Permission;
using API.DOMAIN;
using API.DOMAIN.DTOs.Permission;
using AutoMapper;

namespace API.Mapper.Permission.RolePermission
{
    public class RolePermissionProfile : Profile
    {
        public RolePermissionProfile()
        {
            CreateMap<RolePermissions, CreateRolePermissionCommandResponse>();
            CreateMap<RolePermissions, DeleteRoleCommandResponse>();
            CreateMap<RolePermissions, UpdateRoleCommandResponse>();
            CreateMap<RolePermissionDTO, RolePermissionResponseViewModel>();
            CreateMap<RolePermissionRequestViewModel, DanhMucFilterParam>();
        }
    }
}