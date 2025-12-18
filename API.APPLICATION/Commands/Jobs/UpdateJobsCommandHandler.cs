using API.APPLICATION.Commands.Customer;
using API.APPLICATION.Commands.JobApplys;
using API.Extension;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Jobs
{
    internal class UpdateJobsCommandHandler : IRequestHandler<UpdateJobsCommand, MethodResult<UpdateJobsCommandResponse>>
    {
        private readonly IJobsRepository _jobsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateJobsCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IJobsRepository jobsRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jobsRepository = jobsRepository;
        }

        public async Task<MethodResult<UpdateJobsCommandResponse>> Handle(UpdateJobsCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateJobsCommandResponse>();
            var isExistData = await _jobsRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Id)
                    });
                return methodResult;
            }

            isExistData.SetCode(request.Code);
            isExistData.SetName(request.Name);
            isExistData.SetCategory(request.Category);
            isExistData.SetTimeStart(request.TimeStart);
            isExistData.SetTimeZone(request.TimeZone);
            isExistData.SetJobNumber(request.JobNumber);
            isExistData.SetJobNumberRemain(request.JobNumberRemain);
            isExistData.SetIsHome(request.IsHome);
            isExistData.SetIsEating(request.IsEating);
            isExistData.SetProvince(request.Province);
            isExistData.SetDistrict(request.District);
            isExistData.SetVillage(request.Village);
            isExistData.SetAddress(request.Address);
            isExistData.SetTypeJob(request.TypeJob);
            isExistData.SetPriceFrom(request.PriceFrom);
            isExistData.SetPriceTo(request.PriceTo);
            isExistData.SetTypePayment(request.TypePayment);
            isExistData.SetTimePayment(request.TimePayment);
            isExistData.SetDetail(request.Detail);
            isExistData.SetRequire(request.Require);
            isExistData.SetCancel(request.Cancel);
            isExistData.SetTools(request.Tools);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UpdateJobsCommandResponse>(isExistData);
            return methodResult;
        }
    }
}
