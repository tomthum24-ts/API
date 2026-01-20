using API.DOMAIN;
using API.INFRASTRUCTURE.Interface;
using BaseCommon.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.JobsCategory.DeleteJobsCategory
{
    public class DeleteJobsCategoryCommandHandler : IRequestHandler<DeleteJobsCategoryCommand, MethodResult<DeleteJobsCategoryCommandResponse>>
    {
        private readonly IJobsCategoryRepository _jobsCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteJobsCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IJobsCategoryRepository jobsCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jobsCategoryRepository = jobsCategoryRepository;
        }

        public async Task<MethodResult<DeleteJobsCategoryCommandResponse>> Handle(DeleteJobsCategoryCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteJobsCategoryCommandResponse>();
            // Use Get with predicate to find entities by IDs. Assuming Get takes Expression<Func<T, bool>>
            var existingCategories = await _jobsCategoryRepository.Get(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
            
            if (existingCategories == null || existingCategories.Count == 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(JobsCategory), request.Ids)
                    });
                return methodResult;
            }

            _jobsCategoryRepository.DeleteRange(existingCategories);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            
            return methodResult;
        }
    }
}
