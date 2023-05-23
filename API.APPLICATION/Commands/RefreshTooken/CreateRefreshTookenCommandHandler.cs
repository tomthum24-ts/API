using API.APPLICATION.Commands.RefreshTooken;
using API.DOMAIN;
using API.INFRASTRUCTURE.Interface.RefreshTooken;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
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

        public CreateRefreshTookenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IRefreshTookenRepository refreshTookenRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _refreshTookenRepository = refreshTookenRepository;
        }

        public async Task<MethodResult<CreateRefreshTookenCommandResponse>> Handle(CreateRefreshTookenCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateRefreshTookenCommandResponse>();
            var createUser = new RefreshToken(
                request.Token,
                request.RefreshToken,
                request.Expires,
                request.IpAddress
               );
            _refreshTookenRepository.Add(createUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateRefreshTookenCommandResponse>(createUser);
            return methodResult;
        }
    }
}
