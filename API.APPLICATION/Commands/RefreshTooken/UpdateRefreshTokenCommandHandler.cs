using API.DOMAIN;
using API.DOMAIN.DTOs.User;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface.RefreshToken;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.RefreshToken
{
    public class UpdateRefreshTokenCommandHandler : IRequestHandler<UpdateRefreshTokenCommand, MethodResult<UpdateRefreshTokenCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJWTManagerRepository _jWTManagerRepository;
        private readonly IUserRepository _userRepository;

        public UpdateRefreshTokenCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IRefreshTokenRepository refreshTokenRepository, IJWTManagerRepository jWTManagerRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _refreshTokenRepository = refreshTokenRepository;
            _jWTManagerRepository = jWTManagerRepository;
            _userRepository = userRepository;
        }

        public async Task<MethodResult<UpdateRefreshTokenCommandResponse>> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateRefreshTokenCommandResponse>();
            var existingRefresh = await _refreshTokenRepository.Get(x => x.IdRefreshToken == request.RefreshToken).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            if (existingRefresh == null)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.RefreshToken), request.RefreshToken)
                    });
                return methodResult;
            }
            if(existingRefresh.Expires < DateTime.UtcNow.AddHours(7))
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB04), new[]
                   {
                        ErrorHelpers.GenerateErrorResult(nameof(request.RefreshToken), request.RefreshToken)
                    });
                return methodResult;
            }
            if (existingRefresh.IsRevoked == true)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB05), new[]
                   {
                        ErrorHelpers.GenerateErrorResult(nameof(request.RefreshToken), request.RefreshToken)
                    });
                return methodResult;
            }

            var existingUser = await _userRepository.Get(x => x.UserName == existingRefresh.UserLogin).FirstOrDefaultAsync(cancellationToken);
            var paramUser = new Users();
            paramUser.UserName = existingUser.UserName;
            paramUser.Password = existingUser.PassWord;
            var genToken = await _jWTManagerRepository.GenerateJWTTokens(paramUser, cancellationToken);
            methodResult.Result = _mapper.Map<UpdateRefreshTokenCommandResponse>(genToken);
            return methodResult;
        }
    }
}
