using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Login
{
    public class FogotPasswordCommand : IRequest<MethodResult<FogotPasswordCommandResponse>>
    {
        public string Email {  get; set; }
    }
    public class FogotPasswordCommandResponse
    {
    }
}
