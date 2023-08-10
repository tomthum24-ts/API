using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.RolePermission.Role
{
    public class UpdateRoleCommand : IRequest<MethodResult<UpdateRoleCommandResponse>>
    {
        public int Id { get; set; }
        public string NameController { get; set; }
        public string ActionName { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
    }

    public class UpdateRoleCommandResponse : UpdateRoleCommand
    {
    }
}