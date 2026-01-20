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

namespace API.APPLICATION.Commands.JobsCategory.UpdateJobsCategory
{
    public class UpdateJobsCategoryCommandHandler : IRequestHandler<UpdateJobsCategoryCommand, MethodResult<UpdateJobsCategoryCommandResponse>>
    {
        private readonly IJobsCategoryRepository _jobsCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateJobsCategoryCommandHandler(IMapper mapper, IJobsCategoryRepository jobsCategoryRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _jobsCategoryRepository = jobsCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<MethodResult<UpdateJobsCategoryCommandResponse>> Handle(UpdateJobsCategoryCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateJobsCategoryCommandResponse>();
            var isExistData = await _jobsCategoryRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(JobsCategory), request.Id)
                    });
                return methodResult;
            }
            var existingCategory = await _jobsCategoryRepository.Get(x => x.Code == request.Code && x.Id != request.Id).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            if (existingCategory != null)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.Code), request.Code)
                    });
                return methodResult;
            }

            isExistData.SetCode(request.Code);
            isExistData.SetName(request.Name);
            isExistData.SetNote(request.Note);
            
            _jobsCategoryRepository.Update(isExistData);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UpdateJobsCategoryCommandResponse>(isExistData);
            return methodResult;
        }
    }
}
