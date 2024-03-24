using API.APPLICATION.Commands.WareHouseIn;
using API.DOMAIN;
using API.DOMAIN.DomainObjects.WareHouseInDetail;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public class CreateWareHouseInCommandHandler : IRequestHandler<CreateWareHouseInCommand, MethodResult<CreateWareHouseInCommandResponse>>
    {
        private readonly IWareHouseInRepository _wareHouseInRepository;
        private readonly IWareHouseInDetailRepository _wareHouseInDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWareHouseInCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IWareHouseInRepository wareHouseInRepository, IWareHouseInDetailRepository wareHouseInDetailRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _wareHouseInRepository = wareHouseInRepository;
            _wareHouseInDetailRepository = wareHouseInDetailRepository;
        }

        public async Task<MethodResult<CreateWareHouseInCommandResponse>> Handle(CreateWareHouseInCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateWareHouseInCommandResponse>();
            //bool existingCode = await _wareHouseInRepository.Get(x => x.Code == request.Code).AnyAsync(cancellationToken);
            //if (existingCode)
            //{
            //    methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
            //        {
            //            ErrorHelpers.GenerateErrorResult(nameof(request.Code), request.Code)
            //        });
            //    return methodResult;
            //}
            var createWareHouse = new WareHouseIn(
                    request.Code,
                    request.DateCode,
                    request.CustomerID,
                    request.Representative,
                    request.IntendTime,
                    request.WareHouse,
                    request.CustomerName,
                    request.FilePath,
                    request.FileName,
                    request.Seal,
                    request.Temp,
                    request.CarNumber,
                    request.Container,
                    request.Door,
                    request.Deliver,
                    request.Veterinary,
                    request.Cont,
                    request.Note,
                    request.OrtherNote,
                    request.FileAttach,
                    request.NumberCode,
                    request.InvoiceNumber,
                    request.TimeStart,
                    request.TimeEnd
                );
            _wareHouseInRepository.Add(createWareHouse);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            List<WareHouseInDetail> lstDetail = new List<WareHouseInDetail>();
            if (request.WareHouseInDetail.Count < 1)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB07), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.Code), request.Code)
                    });
                return methodResult;
            }
            foreach (var item in request.WareHouseInDetail)
            {
                foreach (var item2 in item.VehicleDetails)
                {
                    var createDetail = new WareHouseInDetail(
                                createWareHouse.Id,
                                item.RangeOfVehicle,
                                null,
                                item2.ProductId,
                                item2.QuantityProduct,
                                item2.Unit,
                                item2.Size,
                                item2.Weight,
                                item.GuildId
                            );
                    lstDetail.Add(createDetail);
                }
            }
            _wareHouseInDetailRepository.AddRange(lstDetail);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateWareHouseInCommandResponse>(request);
            return methodResult;
        }
    }
}