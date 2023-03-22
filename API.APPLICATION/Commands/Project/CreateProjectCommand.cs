
using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.Project
{
    public class CreateProjectCommand : IRequest<MethodResult<CreateProjectCommandResponse>>
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
    }
    public class CreateProjectCommandResponse : CreateProjectCommand
    {

    }
}
