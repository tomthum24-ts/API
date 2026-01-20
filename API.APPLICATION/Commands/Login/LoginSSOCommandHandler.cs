using API.DOMAIN.DTOs.User;
using API.Extension;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface.RefreshToken;
using AutoMapper;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.EnCrypt;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using BaseCommon.Utilities;
using BitMiracle.LibTiff.Classic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shyjus.BrowserDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Login
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
            if (request.Type == 1)
            {

            }
            var methodResult = new MethodResult<LoginCommandResponse>();
            //var existingUser = await _userRepository.Get(x => x.UserName == request.UserName.ToLower() && x.PassWord == CommonBase.ToMD5(request.Password)).FirstOrDefaultAsync(cancellationToken);
            //if (existingUser == null)
            //{
            //    methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
            //          {
            //            ErrorHelpers.GenerateErrorResult(nameof(User), request.AccessToken),
            //        });
            //    return methodResult;
            //}

            //var ip = _getInfoHelpers?.IpAddress();
            //var paramUser = new Users();
            //paramUser.UserName = request.UserName;
            //paramUser.Password = CommonBase.ToMD5(request.Password);
            //var genToken = await _jWTManagerRepository.GenerateJWTTokens(paramUser, cancellationToken);

            //#region Refresh Token

            //var createUser = _jWTManagerRepository.GenerateRefreshToken(ip, request.UserName);
            //_refreshTokenRepository.Add(createUser);
            //await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            //#endregion Refresh Token

            //genToken.RefreshToken = createUser.IdRefreshToken;
            //methodResult.Result = _mapper.Map<LoginCommandResponse>(genToken);

            return methodResult;
        }
    }
}
