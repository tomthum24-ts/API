
using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.RolePermission.Credential
{
    public class CreateCredentialCommand : IRequest<MethodResult<CreateCredentialCommandResponse>>
    {
        public int UserGroupId { get; set; }
        public int RoleId { get; set; }
        public bool? Status { get; set; }
        public string Note { get; set; }
    }
    public class CreateCredentialCommandResponse : CreateCredentialCommand
    {

    }
}
