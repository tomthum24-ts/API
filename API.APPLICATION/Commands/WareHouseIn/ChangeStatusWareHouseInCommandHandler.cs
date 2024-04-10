using API.DOMAIN;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.WareHouseIn
{
    public class ChangeStatusWareHouseInCommandHandler : IRequestHandler<ChangeStatusWareHouseInCommand, MethodResult<ChangeStatusWareHouseInCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWareHouseInRepository _wareHouseInRepository;

        public ChangeStatusWareHouseInCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWareHouseInRepository wareHouseInRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _wareHouseInRepository = wareHouseInRepository;
        }

        public async Task<MethodResult<ChangeStatusWareHouseInCommandResponse>> Handle(ChangeStatusWareHouseInCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<ChangeStatusWareHouseInCommandResponse>();
            var isExistData = await _wareHouseInRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Id)
                    });
                return methodResult;
            }

            isExistData.SetStep(request.Status);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<ChangeStatusWareHouseInCommandResponse>(request);
            return methodResult;
        }
    }
}