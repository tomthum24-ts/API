using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.JobsCategory.UpdateJobsCategory
{
    public class UpdateJobsCategoryCommand : IRequest<MethodResult<UpdateJobsCategoryCommandResponse>>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
    }

    public class UpdateJobsCategoryCommandResponse : UpdateJobsCategoryCommand
    {
    }
}
