using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Project
{
    public class UpdateProjectCommand : IRequest<MethodResult<UpdateProjectCommandResponse>>
    {
        public int Id { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
    }
    public class UpdateProjectCommandResponse : UpdateProjectCommand
    {
    }
}
