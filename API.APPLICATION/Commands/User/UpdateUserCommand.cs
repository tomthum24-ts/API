using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.User
{
    public class UpdateUserCommand : IRequest<MethodResult<UpdateUserCommandResponse>>
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Status
        {
            get; set;
        }
    }
    public class UpdateUserCommandResponse : UpdateUserCommand
    {
    }
}
