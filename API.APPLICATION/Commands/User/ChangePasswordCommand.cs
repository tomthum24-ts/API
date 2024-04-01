using BaseCommon.Common.MethodResult;
using MediatR;
using Newtonsoft.Json;

namespace API.APPLICATION.Commands.User
{
    public class ChangePasswordCommand : IRequest<MethodResult<ChangePasswordCommandResponse>>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
    public class ChangePasswordCommandResponse : ChangePasswordCommand
    {
    }
}
