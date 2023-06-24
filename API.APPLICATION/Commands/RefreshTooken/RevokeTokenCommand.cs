
using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.RefreshToken
{
    public class RevokeTokenCommand : IRequest<MethodResult<RevokeTokenCommandResponse>>
    {
        public string RefreshToken { get; set; }
    }

    public class RevokeTokenCommandResponse : RevokeTokenCommand
    {
       
    }
}
