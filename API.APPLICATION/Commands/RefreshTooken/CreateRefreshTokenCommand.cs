using BaseCommon.Common.MethodResult;
using MediatR;
using System;

namespace API.APPLICATION.Commands.RefreshToken
{
    public class CreateRefreshTokenCommand : IRequest<MethodResult<CreateRefreshTokenCommandResponse>>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? Expires { get; set; }
        public bool? isExpired { get; set; }
        public string IpAddress { get; set; }
    }
    public class CreateRefreshTokenCommandResponse
    {

    }
}
