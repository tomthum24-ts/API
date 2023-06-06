using API.DOMAIN;
using API.DOMAIN.DTOs.User;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface.RefreshTooken;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.RefreshTooken
{
    public class UpdateRefreshTookenCommandHandler : IRequestHandler<UpdateRefreshTookenCommand, MethodResult<UpdateRefreshTookenCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTookenRepository _refreshTookenRepository;
        private readonly IJWTManagerRepository _jWTManagerRepository;
        private readonly IUserRepository _userRepository;

        public UpdateRefreshTookenCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IRefreshTookenRepository refreshTookenRepository, IJWTManagerRepository jWTManagerRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _refreshTookenRepository = refreshTookenRepository;
            _jWTManagerRepository = jWTManagerRepository;
            _userRepository = userRepository;
        }

        public async Task<MethodResult<UpdateRefreshTookenCommandResponse>> Handle(UpdateRefreshTookenCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateRefreshTookenCommandResponse>();
            var existingRefresh = await _refreshTookenRepository.Get(x => x.IdRefreshToken == request.RefreshToken).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            if (existingRefresh == null)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.RefreshToken), request.RefreshToken)
                    });
                return methodResult;
            }
            if(existingRefresh.Expires < DateTime.UtcNow)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB04), new[]
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
            methodResult.Result = _mapper.Map<UpdateRefreshTookenCommandResponse>(genToken);
            return methodResult;
        }
    }
}
