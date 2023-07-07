using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.RolePermission.Credential
{
    public class UpdateCredentialCommand : IRequest<MethodResult<UpdateCredentialCommandResponse>>
    {
        public int Id { get; set; }
        public int UserGroupId { get; set; }
        public int RoleId { get; set; }
        public bool Status { get; set; }
        public string Note { get; set; }
    }
    public class UpdateCredentialCommandResponse : UpdateCredentialCommand
    {
    }
}
