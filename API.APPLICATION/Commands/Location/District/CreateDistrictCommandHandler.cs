using API.APPLICATION.Commands.Location.District;
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

namespace API.APPLICATION.Commands
{
    public class CreateDistrictCommandHandler : IRequestHandler<CreateDistrictCommand, MethodResult<CreateDistrictCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IDistrictRepository _DistrictRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDistrictCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IDistrictRepository DistrictRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _DistrictRepository = DistrictRepository;
        }


        public async Task<MethodResult<CreateDistrictCommandResponse>> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateDistrictCommandResponse>();
            bool existingUser = await _DistrictRepository.Get(x => x.DistrictCode == request.DistrictCode).AnyAsync(cancellationToken);
            if (existingUser)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.DistrictCode), request.DistrictCode)
                    });
                return methodResult;
            }
            var createDistrict = new District(request.DistrictCode,
            request.DistrictName,
            request.CodeName,
            request.DivisionType,
            request.IdProvince,
            request.Note,
            request.Status);
            _DistrictRepository.Add(createDistrict);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateDistrictCommandResponse>(createDistrict);
            return methodResult;
        }
    }
}
