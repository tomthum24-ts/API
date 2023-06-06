using API.APPLICATION.Commands.RefreshToken;
using API.DOMAIN;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface.RefreshToken;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.MethodResult;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, MethodResult<CreateRefreshTokenCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private IUserSessionInfo _userSessionInfo;
        private readonly IUserRepository _userRepository;

        public CreateRefreshTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IRefreshTokenRepository refreshTokenRepository, IUserSessionInfo userSessionInfo, IUserRepository userRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _refreshTokenRepository = refreshTokenRepository;
            _userSessionInfo = userSessionInfo;
            _userRepository = userRepository;
        }

        public async Task<MethodResult<CreateRefreshTokenCommandResponse>> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var userName = _userSessionInfo.UserName;
            var methodResult = new MethodResult<CreateRefreshTokenCommandResponse>();
            var createUser = new RefreshToken(
                request.RefreshToken,
                request.Expires,
                request.IpAddress,
                userName,
                null,
                null,
                null,
                true
               );
            _refreshTokenRepository.Add(createUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateRefreshTokenCommandResponse>(createUser);
            return methodResult;
        }
    }
}
