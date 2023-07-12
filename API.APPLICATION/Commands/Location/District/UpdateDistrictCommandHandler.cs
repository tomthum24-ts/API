
using API.DOMAIN;
using API.INFRASTRUCTURE.Interface.Location;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Location.District
{
    public class UpdateDistrictCommandHandler : IRequestHandler<UpdateDistrictCommand, MethodResult<UpdateDistrictCommandResponse>>
    {
        private readonly IDistrictRepository _DistrictRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDistrictCommandHandler(IMapper mapper, IDistrictRepository DistrictRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _DistrictRepository = DistrictRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<MethodResult<UpdateDistrictCommandResponse>> Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateDistrictCommandResponse>();
            var isExistData = await _DistrictRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Id)
                    });
                return methodResult;
            }
            bool existingDistrict = await _DistrictRepository.Get(x => x.DistrictCode == request.DistrictCode && x.Id != request.Id).AnyAsync(cancellationToken);
            if (existingDistrict)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.Id), request.Id)
                    });
                return methodResult;
            }
            isExistData.SetDistrictCode(request.DistrictCode);
            isExistData.SetDistrictName(request.DistrictName);
            isExistData.SetCodeName(request.CodeName);
            isExistData.SetDivisionType(request.DivisionType);
            isExistData.SetIdProvince(request.IdProvince);
            isExistData.SetNote(request.Note);
            isExistData.SetStatus(request.Status);
            _DistrictRepository.Update(isExistData);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UpdateDistrictCommandResponse>(isExistData);
            return methodResult;
        }
    }
}
