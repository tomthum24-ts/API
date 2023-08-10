
using BaseCommon.Common.MethodResult;
using MediatR;
using System.Collections.Generic;

namespace API.APPLICATION.Commands.RolePermission.Credential
{
    public class CreateCredentialCommand : IRequest<MethodResult<IEnumerable< CreateCredentialCommandResponse>>>
    {
        public int UserGroupId { get; set; }
        public List<int> CreateCredentials { get; set; }
    }
    public class CreateCredentialCommandResponse : CreateCredentialCommand
    {

    }
}
