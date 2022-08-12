using API.HRM.DOMAIN;
using API.HRM.DOMAIN.DTOs.User;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.EnCrypt;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

        public LoginCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, IJWTManagerRepository jWTManagerRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
            _jWTManagerRepository = jWTManagerRepository;
        }

        public async Task<MethodResult<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<LoginCommandResponse>();
            var existingUser= await _userRepository.Get(x => x.UserName == request.UserName && x.PassWord == CommonBase.ToMD5(request.Password)).FirstOrDefaultAsync(cancellationToken);
            if (existingUser == null)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                      {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.UserName),
                    });
                return methodResult;
            }
            var paramUser = new Users();
            paramUser.UserName = request.UserName;
            paramUser.Password = request.Password;
            var genToken = await _jWTManagerRepository.GenerateJWTTokens(paramUser, cancellationToken);
            methodResult.Result = _mapper.Map<LoginCommandResponse>(genToken);
            return methodResult;
        }
    }
}
