using BaseCommon.Common.MethodResult;
using MediatR;
using System.Collections.Generic;

namespace API.APPLICATION.Commands.JobsCategory.DeleteJobsCategory
{
    public class DeleteJobsCategoryCommand : IRequest<MethodResult<DeleteJobsCategoryCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }

    public class DeleteJobsCategoryCommandResponse : DeleteJobsCategoryCommand
    {
    }
}
