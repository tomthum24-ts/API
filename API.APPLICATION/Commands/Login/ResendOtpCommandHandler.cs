using BaseCommon.Common.MethodResult;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using API.INFRASTRUCTURE;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BaseCommon.UnitOfWork;
using BaseCommon.Enums;
using API.DOMAIN;
using BaseCommon.UnitOfWork;
using BaseCommon.Common.EnCrypt;
using BaseCommon.Extension;

namespace API.APPLICATION.Commands.Login
{
    public class ResendOtpCommandHandler : IRequestHandler<ResendOtpCommand, MethodResult<ResendOtpCommandResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ResendOtpCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<MethodResult<ResendOtpCommandResponse>> Handle(ResendOtpCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<ResendOtpCommandResponse>();
            var isExistData = await _userRepository.Get(x => x.Email == request.Email).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Email)
                    });
                return methodResult;
            }
            try
            {
                SendEmailExtension.SendMail(request.Email, "Otp của bạn là: " + isExistData.OTP.ToString(), "Kích hoạt tài khoản VietColdChain ", "smtp.gmail.com", 587);
            }
            catch (System.Exception)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB12), new[]
                  {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Email)
                    });
                return methodResult;
            }
            return methodResult;
        }
    }
}