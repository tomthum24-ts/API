using API.APPLICATION.Commands.Login;
using API.DOMAIN;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Login
{
    public class LockAccountCommandHandler : IRequestHandler<LockAccountCommand, MethodResult<LockAccountCommandResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSessionInfo _userSessionInfo;

        public LockAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, IUserSessionInfo userSessionInfo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
            _userSessionInfo = userSessionInfo;
        }

        public async Task<MethodResult<LockAccountCommandResponse>> Handle(LockAccountCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<LockAccountCommandResponse>();
            var user= _userSessionInfo?.ID.Value ?? 0;
            var isExistData = await _userRepository.Get(x => x.Id == user).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), " ")
                    });
                return methodResult;
            }
            _userRepository.Delete(isExistData);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return methodResult;
        }
    }
}
