using API.APPLICATION.Commands.Login;
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

namespace API.APPLICATION.Commands.User
{
    public class ActiveAccountCommandHandler : IRequestHandler<ActiveAccountCommand, MethodResult<ActiveAccountCommandResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ActiveAccountCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<MethodResult<ActiveAccountCommandResponse>> Handle(ActiveAccountCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<ActiveAccountCommandResponse>();
            var isExistData = await _userRepository.Get(x => x.Email == request.Email && x.OTP==request.OTP).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB10), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Email)
                    });
                return methodResult;
            }
            isExistData.SetStatus(true);
            _userRepository.Update(isExistData);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return methodResult;
        }
    }
}