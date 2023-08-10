using BaseCommon.Common.MethodResult;
using MediatR;
using System.Collections.Generic;

namespace API.APPLICATION.Commands.RolePermission.Credential
{
    public class UpdateCredentialCommand : IRequest<MethodResult<IEnumerable<UpdateCredentialCommandResponse>>>
    {
        public int UserGroupId { get; set; }
        public List<UpdateCredential>updateCredentials { get; set; }
    }
    public class UpdateCredential
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
