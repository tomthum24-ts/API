using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Project
{
    public class DeleteProjectCommand : IRequest<MethodResult<DeleteProjectCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }
    public class DeleteProjectCommandResponse : DeleteProjectCommand
    { 
    }
}
