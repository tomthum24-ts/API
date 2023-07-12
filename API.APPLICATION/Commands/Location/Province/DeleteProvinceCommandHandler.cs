using API.DOMAIN;
using API.INFRASTRUCTURE.Interface.Location;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Location.Province
{
    public class DeleteProvinceCommandHandler : IRequestHandler<DeleteProvinceCommand, MethodResult<DeleteProvinceCommandResponse>>
    {
        private readonly IProvinceRepository _ProvinceRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProvinceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IProvinceRepository ProvinceRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ProvinceRepository = ProvinceRepository;
        }

        public async Task<MethodResult<DeleteProvinceCommandResponse>> Handle(DeleteProvinceCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteProvinceCommandResponse>();
            var existingUser = await _ProvinceRepository.Get(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
            if (existingUser == null || existingUser.Count == 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Ids)
                    });
                return methodResult;
            }
            _ProvinceRepository.DeleteRange(existingUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<DeleteUserCommandResponse>(existingUser);
            return methodResult;
        }
    }
}
