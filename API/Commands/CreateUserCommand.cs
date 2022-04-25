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
    public class CreateUserCommand: IRequest<MethodResult<CreateUserCommandResponse>>
    {
        public string UserName { get; set; }
        public string HoDem { get; set; }
        public string Ten { get; set; }
    }
    public class CreateUserCommandResponse : UserDTO
    {

    }
 

}
