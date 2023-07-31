using API.APPLICATION.Commands.SysBieuMau;
using API.Extension;
using API.INFRASTRUCTURE.Interface.BieuMau;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands
{
    public class DeleteSysBieuMauCoQuanCommandHandler : IRequestHandler<DeleteSysBieuMauCoQuanCommand, MethodResult<DeleteSysBieuMauCoQuanCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        protected readonly ISysBieuMauRepository _sysBieuMauRepository;
        private readonly IConfiguration _configuration;

        public async Task<MethodResult<DeleteSysBieuMauCoQuanCommandResponse>> Handle(DeleteSysBieuMauCoQuanCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteSysBieuMauCoQuanCommandResponse>();

            #region Validate

            var existingSysBieuMaus = await _sysBieuMauRepository.Get(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
            if (existingSysBieuMaus == null || existingSysBieuMaus.Count() != request.Ids.Count())
            {
                methodResult.AddAPIErrorMessage(nameof(ESysBieuMauErrorCode.SBMC02), new[]
                {
                        ErrorHelpers.GenerateErrorResult(nameof(SysBieuMau), string.Join(", ", request.Ids))
                    });
                throw new CommandHandlerException(methodResult.ErrorMessages);
            }

            #endregion Validate

            _sysBieuMauRepository.DeleteRange(existingSysBieuMaus);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return methodResult;
        }
    }
}