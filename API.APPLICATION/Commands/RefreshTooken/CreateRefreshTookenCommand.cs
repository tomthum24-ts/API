using BaseCommon.Common.MethodResult;
using MediatR;
using System;

namespace API.APPLICATION.Commands.RefreshTooken
{
    public class CreateRefreshTookenCommand : IRequest<MethodResult<CreateRefreshTookenCommandResponse>>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? Expires { get; set; }
        public bool? isExpired { get; set; }
        public string IpAddress { get; set; }
    }
    public class CreateRefreshTookenCommandResponse
    {

    }
}
