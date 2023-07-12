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

namespace API.APPLICATION.Commands.Location.District
{
    public class DeleteDistrictCommandHandler : IRequestHandler<DeleteDistrictCommand, MethodResult<DeleteDistrictCommandResponse>>
    {
        private readonly IDistrictRepository _DistrictRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDistrictCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IDistrictRepository DistrictRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _DistrictRepository = DistrictRepository;
        }

        public async Task<MethodResult<DeleteDistrictCommandResponse>> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteDistrictCommandResponse>();
            var existingUser = await _DistrictRepository.Get(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
            if (existingUser == null || existingUser.Count == 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Ids)
                    });
                return methodResult;
            }
            _DistrictRepository.DeleteRange(existingUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<DeleteUserCommandResponse>(existingUser);
            return methodResult;
        }
    }
}
