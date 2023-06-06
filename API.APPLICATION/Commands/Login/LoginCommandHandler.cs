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
using API.INFRASTRUCTURE.Interface.RefreshTooken;
using BaseCommon.Common.ClaimUser;

namespace API.APPLICATION.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, MethodResult<LoginCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IJWTManagerRepository _jWTManagerRepository;
        private readonly IRefreshTookenRepository _refreshTookenRepository;
        private IHttpContextAccessor _accessor;
        private IUserSessionInfo _userSessionInfo;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, IJWTManagerRepository jWTManagerRepository, IHttpContextAccessor accessor, IRefreshTookenRepository refreshTookenRepository, IUserSessionInfo userSessionInfo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
            _jWTManagerRepository = jWTManagerRepository;
            _accessor = accessor;
            _refreshTookenRepository = refreshTookenRepository;
            _userSessionInfo = userSessionInfo;
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
            var ip = _accessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            var paramUser = new Users();
            paramUser.UserName = request.UserName;
            paramUser.Password = CommonBase.ToMD5(request.Password);

            var genToken = await _jWTManagerRepository.GenerateJWTTokens(paramUser, cancellationToken);
            genToken.RefreshToken = GenerateRefreshToken(ip).IdRefreshToken;
            methodResult.Result = _mapper.Map<LoginCommandResponse>(genToken);

            #region Refresh Tooken
            var randomBytes = CMSEncryption.RandomBytes();

            var createUser = new RefreshToken(
                   genToken.Token,
                   genToken.RefreshToken,
                   DateTime.UtcNow.AddDays(7),
                   ip,
                   request.UserName,
                   null,
                   null,
                   null,
                   true
                  );
            _refreshTookenRepository.Add(createUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            #endregion

            return methodResult;
        }
        private string IpAddress()
        {
            // get source ip address for the current request
            if (_accessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                return _accessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString();
            else
                return _accessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            var userName = _userSessionInfo.UserName;
            var randomBytes = CMSEncryption.RandomBytes();
            var refreshToken = new RefreshToken(
                  null,
                 Convert.ToBase64String(randomBytes),
                 DateTime.UtcNow.AddDays(7),
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
