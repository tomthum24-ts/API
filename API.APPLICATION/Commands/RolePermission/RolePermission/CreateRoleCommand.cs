using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.RolePermission.Role
{
    public class CreateRolePermissionCommand : IRequest<MethodResult<CreateRolePermissionCommandResponse>>
    {
        public string NameController { get; set; }
        public string ActionName { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
    }

    public class CreateRolePermissionCommandResponse : CreateRolePermissionCommand
    {
    }
}