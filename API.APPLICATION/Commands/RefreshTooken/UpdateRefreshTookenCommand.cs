
using BaseCommon.Common.MethodResult;
using MediatR;
using System;

namespace API.APPLICATION.Commands.RefreshTooken
{
    public class UpdateRefreshTookenCommand : IRequest<MethodResult<UpdateRefreshTookenCommandResponse>>
    {
        public string RefreshToken { get; set; }
    }
    public class UpdateRefreshTookenCommandResponse : UpdateRefreshTookenCommand
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? ExpiresIn { get; set; }
    }
}
