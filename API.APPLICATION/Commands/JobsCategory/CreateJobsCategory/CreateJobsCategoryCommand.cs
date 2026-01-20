using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.JobsCategory.CreateJobsCategory
{
    public class CreateJobsCategoryCommand : IRequest<MethodResult<CreateJobsCategoryCommandResponse>>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
    }

    public class CreateJobsCategoryCommandResponse : CreateJobsCategoryCommand
    {

    }
}
