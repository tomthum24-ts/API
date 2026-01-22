using API.APPLICATION.Commands.Login;
using API.DOMAIN;
using API.DOMAIN.DTOs.User;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface.RefreshToken;
using AutoMapper;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.EnCrypt;
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
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public class LoginSSOCommandHandler : IRequestHandler<LoginSSOCommand, MethodResult<LoginCommandResponse>>
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

        public LoginSSOCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, IJWTManagerRepository jWTManagerRepository, IRefreshTokenRepository refreshTokenRepository, IHttpContextAccessor httpContextAccessor, IUserSessionInfo userSessionInfo, IConfiguration iconfiguration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
            _jWTManagerRepository = jWTManagerRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _httpContextAccessor = httpContextAccessor;
            _userSessionInfo = userSessionInfo;
            _iconfiguration = iconfiguration;
        }


        public async Task<MethodResult<LoginCommandResponse>> Handle(LoginSSOCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<LoginCommandResponse>();
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(request.AccessToken);
            var user = string.Empty;
            var given_name = string.Empty;
            var family_name = string.Empty;
            if(request.Type > 2)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                   {
                        BaseCommon.Common.MethodResult.ErrorHelpers.GenerateErrorResult(nameof(User), request.Type.ToString()),
                    });
                return methodResult;
            }
            if (request.Type == 1)
            {
                foreach (var claim in jwtToken.Claims)
                {
                    if (claim.Type == "email")
                    {
                        user = claim.Value.ToString();
                    }
                    if (claim.Type == "family_name")
                    {
                        family_name = claim.Value.ToString();
                    }
                    if (claim.Type == "given_name")
                    {
                        given_name = claim.Value.ToString();
                    }
                }
            }
            var existingUser = await _userRepository.Get(x => x.UserName == user.ToLower()).FirstOrDefaultAsync(cancellationToken);
            if (existingUser == null)
            {
                var createUser = new User(
                user,
                family_name + given_name,
                given_name,
                user,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null
               );
                _userRepository.Add(createUser);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            var ip = _getInfoHelpers?.IpAddress();
            var paramUser = new Users();
            paramUser.UserName = user;
            //paramUser.Password = CommonBase.ToMD5(request.Password);
            var genToken = await _jWTManagerRepository.GenerateJWTTokens(paramUser, cancellationToken);

            #region Refresh Token

            var createUserToken = _jWTManagerRepository.GenerateRefreshToken(ip, user);
            _refreshTokenRepository.Add(createUserToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            #endregion Refresh Token

            genToken.RefreshToken = createUserToken.IdRefreshToken;
            methodResult.Result = _mapper.Map<LoginCommandResponse>(genToken);

            return methodResult;
        }
    }
}
