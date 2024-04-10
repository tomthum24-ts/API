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

namespace API.APPLICATION.Commands.WareHouseOut
{
    public class ChangeStatusWareHouseOutCommandHandler : IRequestHandler<ChangeStatusWareHouseOutCommand, MethodResult<ChangeStatusWareHouseOutCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWareHouseOutRepository _wareHouseOutRepository;

        public ChangeStatusWareHouseOutCommandHandler(IWareHouseOutRepository wareHouseOutRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _wareHouseOutRepository = wareHouseOutRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MethodResult<ChangeStatusWareHouseOutCommandResponse>> Handle(ChangeStatusWareHouseOutCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<ChangeStatusWareHouseOutCommandResponse>();
            var isExistData = await _wareHouseOutRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
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
            methodResult.Result = _mapper.Map<ChangeStatusWareHouseOutCommandResponse>(request);
            return methodResult;
        }
    }
}
