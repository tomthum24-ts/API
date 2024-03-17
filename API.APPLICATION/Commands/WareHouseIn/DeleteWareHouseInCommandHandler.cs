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
    public class DeleteWareHouseInCommandHandler : IRequestHandler<DeleteWareHouseInCommand, MethodResult<DeleteWareHouseInCommandResponse>>
    {
        private readonly IWareHouseInRepository _wareHouseInRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWareHouseInCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWareHouseInRepository wareHouseInRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _wareHouseInRepository = wareHouseInRepository;
        }

        public async Task<MethodResult<DeleteWareHouseInCommandResponse>> Handle(DeleteWareHouseInCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteWareHouseInCommandResponse>();
            var existingUser = await _wareHouseInRepository.Get(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
            if (existingUser == null || existingUser.Count == 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Ids)
                    });
                return methodResult;
            }
            _wareHouseInRepository.DeleteRange(existingUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<DeleteUserCommandResponse>(existingUser);
            return methodResult;
        }
    }
}