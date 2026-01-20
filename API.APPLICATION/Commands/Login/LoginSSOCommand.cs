using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Login
{
    public class LoginSSOCommand : IRequest<MethodResult<LoginCommandResponse>>
    {
        public int Type { get; set; }
        public string AccessToken { get; set; }
    }
}
