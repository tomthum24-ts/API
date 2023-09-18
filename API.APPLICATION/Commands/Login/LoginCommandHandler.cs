using API.DOMAIN;
using API.DOMAIN.DTOs.User;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface.RefreshToken;
using AutoMapper;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.EnCrypt;
using BaseCommon.Common.HttpDetection;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using BaseCommon.Utilities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shyjus.BrowserDetection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, MethodResult<LoginCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IJWTManagerRepository _jWTManagerRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserSessionInfo _userSessionInfo;
        private readonly IConfiguration _iconfiguration;
        private readonly GetInfoHelpers _getInfoHelpers;
        private readonly IBrowserDetector _browserDetector;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, IJWTManagerRepository jWTManagerRepository, IHttpContextAccessor httpContextAccessor, IRefreshTokenRepository refreshTokenRepository, IUserSessionInfo userSessionInfo, IConfiguration iconfiguration, GetInfoHelpers getInfoHelpers, IBrowserDetector browserDetector)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
            _jWTManagerRepository = jWTManagerRepository;
            _httpContextAccessor = httpContextAccessor;
            _refreshTokenRepository = refreshTokenRepository;
            _userSessionInfo = userSessionInfo;
            _iconfiguration = iconfiguration;
            _getInfoHelpers = getInfoHelpers;
            _browserDetector = browserDetector;
        }

        public async Task<MethodResult<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<LoginCommandResponse>();
            var existingUser = await _userRepository.Get(x => x.UserName == request.UserName.ToLower() && x.PassWord == CommonBase.ToMD5(request.Password)).FirstOrDefaultAsync(cancellationToken);
            if (existingUser == null)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                      {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.UserName),
                    });
                return methodResult;
            }
            

            var ip = _getInfoHelpers?.IpAddress();
            var paramUser = new Users();
            paramUser.UserName = request.UserName;
            paramUser.Password = CommonBase.ToMD5(request.Password);
            var genToken = await _jWTManagerRepository.GenerateJWTTokens(paramUser, cancellationToken);

            #region Refresh Token

            var createUser = _jWTManagerRepository.GenerateRefreshToken(ip, request.UserName);
            _refreshTokenRepository.Add(createUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            #endregion Refresh Token

            genToken.RefreshToken = createUser.IdRefreshToken;
            methodResult.Result = _mapper.Map<LoginCommandResponse>(genToken);
            return methodResult;
        }

    }
}