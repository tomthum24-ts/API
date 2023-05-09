

using API.APPLICATION.Commands.Location.Province;
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

namespace API.APPLICATION.Commands
{
    public class CreateProvinceCommandHandler : IRequestHandler<CreateProvinceCommand, MethodResult<CreateProvinceCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IProvinceRepository _ProvinceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProvinceCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IProvinceRepository ProvinceRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _ProvinceRepository = ProvinceRepository;
        }


        public async Task<MethodResult<CreateProvinceCommandResponse>> Handle(CreateProvinceCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateProvinceCommandResponse>();
            bool existingUser = await _ProvinceRepository.Get(x => x.ProvinceCode == request.ProvinceCode).AnyAsync(cancellationToken);
            if (existingUser)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.ProvinceCode), request.ProvinceCode)
                    });
                return methodResult;
            }
            var createProvince = new Province(
                                                request.ProvinceCode,
                                                request.ProvinceName,
                                                request.CodeName,
                                                request.DivisionType,
                                                request.Note,
                                                request.Status
            );
            _ProvinceRepository.Add(createProvince);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateProvinceCommandResponse>(createProvince);
            return methodResult;
        }
    }
}
