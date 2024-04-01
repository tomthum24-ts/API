using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.Login
{
    public class ActiveAccountCommand : IRequest<MethodResult<ActiveAccountCommandResponse>>
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }

    public class ActiveAccountCommandResponse
    {
    }
}