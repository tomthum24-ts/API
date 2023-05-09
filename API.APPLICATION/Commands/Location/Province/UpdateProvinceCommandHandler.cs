using API.DOMAIN;
using API.INFRASTRUCTURE.Interface.Location;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Location.Province
{
    public class UpdateProvinceCommandHandler : IRequestHandler<UpdateProvinceCommand, MethodResult<UpdateProvinceCommandResponse>>
    {
        private readonly IProvinceRepository _ProvinceRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProvinceCommandHandler(IMapper mapper, IProvinceRepository ProvinceRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _ProvinceRepository = ProvinceRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<MethodResult<UpdateProvinceCommandResponse>> Handle(UpdateProvinceCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateProvinceCommandResponse>();
            var isExistData = await _ProvinceRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Id)
                    });
                return methodResult;
            }
            bool existingProvince = await _ProvinceRepository.Get(x => x.ProvinceCode == request.ProvinceCode && x.Id != request.Id).AnyAsync(cancellationToken);
            if (existingProvince)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.Id), request.Id)
                    });
                return methodResult;
            }
            isExistData.SetProvinceCode(request.ProvinceCode);
            isExistData.SetProvinceName(request.ProvinceName);
            isExistData.SetCodeName(request.CodeName);
            isExistData.SetDivisionType(request.DivisionType);
            isExistData.SetNote(request.Note);
            isExistData.SetStatus(request.Status);
            _ProvinceRepository.Update(isExistData);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UpdateProvinceCommandResponse>(isExistData);
            return methodResult;
        }
    }
}
