using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Login
{
    public class LoginCommand : IRequest<MethodResult<LoginCommandResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginCommandResponse 
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? ExpiresIn { get; set; }
    }
}
