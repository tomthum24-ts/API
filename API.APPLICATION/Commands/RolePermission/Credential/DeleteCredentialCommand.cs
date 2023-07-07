using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.RolePermission.Credential
{
    public class DeleteCredentialCommand : IRequest<MethodResult<DeleteCredentialCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }
    public class DeleteCredentialCommandResponse : DeleteCredentialCommand
    {
        public DeleteCredentialCommandResponse(List<DeleteCredentialCommand> datas)
        {
            Datas = datas;
        }
        public List<DeleteCredentialCommand> Datas { get; }
    }
}
