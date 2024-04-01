using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.Login
{
    public class ResendOtpCommand : IRequest<MethodResult<ResendOtpCommandResponse>>
    {
        public string Email { get; set; }
    }

    public class ResendOtpCommandResponse
    {
    }
}