using API.DOMAIN;
using API.DOMAIN.DTOs.User;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface.RefreshToken;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.RefreshToken
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, MethodResult<RevokeTokenCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly GetInfoHelpers _getInfoHelpers;


        public RevokeTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork, GetInfoHelpers getInfoHelpers)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
            _getInfoHelpers = getInfoHelpers;
        }

        public async Task<MethodResult<RevokeTokenCommandResponse>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<RevokeTokenCommandResponse>();
            var existingRevoke = await _refreshTokenRepository.Get(x => x.IdRefreshToken == request.RefreshToken).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            if (existingRevoke == null)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.RefreshToken), request.RefreshToken)
                    });
                return methodResult;
            }
            existingRevoke.SetIsRevoked(true);
            existingRevoke.SetRevokedByIp(_getInfoHelpers.IpAddress());
            existingRevoke.SetRevoked(DateTime.UtcNow);
            _refreshTokenRepository.Update(existingRevoke);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return methodResult;
        }
    }
}
