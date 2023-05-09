using API.APPLICATION.Commands.Location.Village;
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
    public class CreateVillageCommandHandler : IRequestHandler<CreateVillageCommand, MethodResult<CreateVillageCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IVillageRepository _VillageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateVillageCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IVillageRepository VillageRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _VillageRepository = VillageRepository;
        }


        public async Task<MethodResult<CreateVillageCommandResponse>> Handle(CreateVillageCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateVillageCommandResponse>();
            bool existingUser = await _VillageRepository.Get(x => x.VillageCode == request.VillageCode).AnyAsync(cancellationToken);
            if (existingUser)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.VillageCode), request.VillageCode)
                    });
                return methodResult;
            }
            var createVillage = new Village(
                                            request.VillageCode,
                                            request.VillageName,
                                            request.CodeName,
                                            request.DivisionType,
                                            request.IdDistrict,
                                            request.Note,
                                            request.Status
            );
            _VillageRepository.Add(createVillage);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateVillageCommandResponse>(createVillage);
            return methodResult;
        }
    }
}
