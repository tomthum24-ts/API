using API.APPLICATION.Queries.Media;
using API.Extension;
using API.INFRASTRUCTURE.Interface.BieuMau;
using AutoMapper;
using BaseCommon.Common.EnCrypt;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.SysBieuMau
{
    public class CreateOrUpdateSysBieuMauCoQuanCommandHandler : IRequestHandler<CreateOrUpdateSysBieuMauCoQuanCommand, MethodResult<CreateOrUpdateSysBieuMauCoQuanCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        protected readonly ISysBieuMauRepository _sysBieuMauRepository;
        private readonly IConfiguration _configuration;

        public CreateOrUpdateSysBieuMauCoQuanCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration, ISysBieuMauRepository sysBieuMauRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _sysBieuMauRepository = sysBieuMauRepository;
        }

        public async Task<MethodResult<CreateOrUpdateSysBieuMauCoQuanCommandResponse>> Handle(CreateOrUpdateSysBieuMauCoQuanCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateOrUpdateSysBieuMauCoQuanCommandResponse>();

            var bieuMau = await _sysBieuMauRepository.Get(x => x.Id == request.IdBieuMau).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            if (bieuMau == null)
            {
                methodResult.AddAPIErrorMessage(nameof(ESysBieuMauErrorCode.SBMC01), new[]
                     {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.MaBieuMau),
                    });
                return methodResult;
            }
            var sizeLimit = _configuration.GetValue<long>("MediaConfig:SizeLimit");
            var typeLimit = _configuration.GetSection("MediaConfig:PermittedExtensions").Get<string[]>();
            var fileType = new FileType();
            string md5 = null;
            FileTypeVerifyResult fileTypeVerifyResult = null;
            byte[]? noiDungBieuMau = null;
            if (request.FileDinhKem != null)
            {
                fileTypeVerifyResult = await fileType.ProcessFormFile(request.FileDinhKem, typeLimit, sizeLimit);
                md5 = EncryptionHelper.MD5(fileTypeVerifyResult.Result);
                noiDungBieuMau = fileTypeVerifyResult.Result;
            }
            bieuMau.SetMaBieuMau(request.MaBieuMau);
            bieuMau.SetNoiDung(noiDungBieuMau);
            bieuMau.SetCheckSum(md5);
            bieuMau.SetIsExportPDF(request.IsExportPDF);
            _sysBieuMauRepository.Update(bieuMau);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            methodResult.Result = new CreateOrUpdateSysBieuMauCoQuanCommandResponse() { Id = bieuMau.Id };
            return methodResult;
        }
    }
}