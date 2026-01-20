using API.APPLICATION.Commands.JobsCategory.CreateJobsCategory;
using API.DOMAIN;
using API.INFRASTRUCTURE.Interface;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public class CreateJobsCategoryCommandHandler : IRequestHandler<CreateJobsCategoryCommand, MethodResult<CreateJobsCategoryCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IJobsCategoryRepository _jobsCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateJobsCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IJobsCategoryRepository jobsCategoryRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jobsCategoryRepository = jobsCategoryRepository;
        }

        public async Task<MethodResult<CreateJobsCategoryCommandResponse>> Handle(CreateJobsCategoryCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateJobsCategoryCommandResponse>();
            // Check if exists
            bool existing = await _jobsCategoryRepository.Get(x => x.Code == request.Code).AnyAsync(cancellationToken);
            if (existing)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
                {
                    ErrorHelpers.GenerateErrorResult(nameof(request.Code), request.Code)
                });
                return methodResult;
            }

            var newEntity = new JobsCategory(
                 request.Code,
                 request.Name,
                 request.Note
                );

            _jobsCategoryRepository.Add(newEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            
            methodResult.Result = _mapper.Map<CreateJobsCategoryCommandResponse>(newEntity);
            return methodResult;
        }
    }
}
