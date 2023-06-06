using API.APPLICATION.Commands.RefreshTooken;
using API.DOMAIN;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface.RefreshTooken;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.MethodResult;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public class CreateRefreshTookenCommandHandler : IRequestHandler<CreateRefreshTookenCommand, MethodResult<CreateRefreshTookenCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTookenRepository _refreshTookenRepository;
        private IUserSessionInfo _userSessionInfo;
        private readonly IUserRepository _userRepository;

        public CreateRefreshTookenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IRefreshTookenRepository refreshTookenRepository, IUserSessionInfo userSessionInfo, IUserRepository userRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _refreshTookenRepository = refreshTookenRepository;
            _userSessionInfo = userSessionInfo;
            _userRepository = userRepository;
        }

        public async Task<MethodResult<CreateRefreshTookenCommandResponse>> Handle(CreateRefreshTookenCommand request, CancellationToken cancellationToken)
        {
            var userName = _userSessionInfo.UserName;
            var methodResult = new MethodResult<CreateRefreshTookenCommandResponse>();
            var createUser = new RefreshToken(
                request.Token,
                request.RefreshToken,
                request.Expires,
                request.IpAddress,
                userName,
                null,
                null,
                null,
                true
               );
            _refreshTookenRepository.Add(createUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateRefreshTookenCommandResponse>(createUser);
            return methodResult;
        }
    }
}
