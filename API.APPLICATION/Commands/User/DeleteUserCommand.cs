using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.User
{
    public class DeleteUserCommand : IRequest<MethodResult<DeleteUserCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }
    public class DeleteUserCommandResponse { }
}
