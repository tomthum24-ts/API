using API.DOMAIN.DTOs.User;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.EnCrypt;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using API.DOMAIN;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Cryptography;
using API.INFRASTRUCTURE.Interface.RefreshToken;
using BaseCommon.Common.ClaimUser;
using Microsoft.Extensions.Configuration;
using BaseCommon.Utilities;

namespace API.APPLICATION.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, MethodResult<LoginCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IJWTManagerRepository _jWTManagerRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private IHttpContextAccessor _accessor;
        private IUserSessionInfo _userSessionInfo;
        private readonly IConfiguration _iconfiguration;
        private readonly GetInfoHelpers _getInfoHelpers;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, IJWTManagerRepository jWTManagerRepository, IHttpContextAccessor accessor, IRefreshTokenRepository refreshTokenRepository, IUserSessionInfo userSessionInfo, IConfiguration iconfiguration, GetInfoHelpers getInfoHelpers)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
            _jWTManagerRepository = jWTManagerRepository;
            _accessor = accessor;
            _refreshTokenRepository = refreshTokenRepository;
            _userSessionInfo = userSessionInfo;
            _iconfiguration = iconfiguration;
            _getInfoHelpers = getInfoHelpers;
        }

        public async Task<MethodResult<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            
            var methodResult = new MethodResult<LoginCommandResponse>();
            var existingUser = await _userRepository.Get(x => x.UserName == request.UserName && x.PassWord == CommonBase.ToMD5(request.Password)).FirstOrDefaultAsync(cancellationToken);
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
            var createUser = GenerateRefreshToken(ip, request.UserName);
            _refreshTokenRepository.Add(createUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            #endregion
            genToken.RefreshToken = createUser.IdRefreshToken;
            methodResult.Result = _mapper.Map<LoginCommandResponse>(genToken);
            return methodResult;
        }
      
        public UserRefreshToken GenerateRefreshToken(string ipAddress,string userName)
        {
            var randomBytes = CMSEncryption.RandomBytes();
            var refreshToken = new UserRefreshToken(
                 Convert.ToBase64String(randomBytes),
                 DateTime.UtcNow.AddMinutes(int.Parse(_iconfiguration["JWT:TimeRefresh"])),
                 ipAddress,
                 userName,
                 null,
                 null,
                 null,
                 true
                );
            return refreshToken;
        }

    }

}
