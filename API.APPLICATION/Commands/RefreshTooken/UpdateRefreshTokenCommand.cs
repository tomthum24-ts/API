
using BaseCommon.Common.MethodResult;
using MediatR;
using System;

namespace API.APPLICATION.Commands.RefreshToken
{
    public class UpdateRefreshTokenCommand : IRequest<MethodResult<UpdateRefreshTokenCommandResponse>>
    {
        public string RefreshToken { get; set; }
    }
    public class UpdateRefreshTokenCommandResponse : UpdateRefreshTokenCommand
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? ExpiresIn { get; set; }
    }
}
