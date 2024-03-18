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
    public class DeleteWareHouseOutCommandHandler : IRequestHandler<DeleteWareHouseOutCommand, MethodResult<DeleteWareHouseOutCommandResponse>>
    {
        private readonly IWareHouseOutRepository _WareHouseOutRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWareHouseOutCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWareHouseOutRepository WareHouseOutRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _WareHouseOutRepository = WareHouseOutRepository;
        }

        public async Task<MethodResult<DeleteWareHouseOutCommandResponse>> Handle(DeleteWareHouseOutCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteWareHouseOutCommandResponse>();
            var existingUser = await _WareHouseOutRepository.Get(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
            if (existingUser == null || existingUser.Count == 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Ids)
                    });
                return methodResult;
            }
            _WareHouseOutRepository.DeleteRange(existingUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<DeleteUserCommandResponse>(existingUser);
            return methodResult;
        }
    }
}