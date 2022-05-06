using API.Common;
using MediatR;
using System.Collections.Generic;

namespace API.Commands
{
    public class DeleteUserCommand: IRequest<MethodResult<DeleteUserCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }
    public class DeleteUserCommandResponse { }
}
