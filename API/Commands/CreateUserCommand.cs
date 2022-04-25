using API.Common;
using API.Data;
using API.Models;
using API.Queries;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Commands
{
    public class CreateUserCommand : IRequest<MethodResult<CreateUserCommandResponse>>
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool? Status
        {
            get; set;
        }
    }
    public class CreateUserCommandResponse : UserViewModel
    {

    }
 

}
