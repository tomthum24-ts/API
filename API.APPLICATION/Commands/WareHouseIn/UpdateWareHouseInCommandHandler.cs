using API.DOMAIN;
using API.DOMAIN.DomainObjects.WareHouseInDetail;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.WareHouseIn
{
    public class UpdateWareHouseInCommandHandler : IRequestHandler<UpdateWareHouseInCommand, MethodResult<UpdateWareHouseInCommandResponse>>
    {
        private readonly IWareHouseInRepository _wareHouseInRepository;
        private readonly IWareHouseInDetailRepository _wareHouseInDetailRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWareHouseInCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWareHouseInRepository wareHouseInRepository, IWareHouseInDetailRepository wareHouseInDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _wareHouseInRepository = wareHouseInRepository;
            _wareHouseInDetailRepository = wareHouseInDetailRepository;
        }

        public async Task<MethodResult<UpdateWareHouseInCommandResponse>> Handle(UpdateWareHouseInCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateWareHouseInCommandResponse>();
            var isExistData = await _wareHouseInRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Id)
                    });
                return methodResult;
            }

            isExistData.SetCode(request.Code);
            isExistData.SetDateCode(request.DateCode);
            isExistData.SetCustomerID(request.CustomerID);
            isExistData.SetRepresentative(request.Representative);
            isExistData.SetIntendTime(request.IntendTime);
            isExistData.SetWareHouse(request.WareHouse);
            isExistData.SetCustomerName(request.CustomerName);
            isExistData.SetFilePath(request.FilePath);
            isExistData.SetFileName(request.FileName);
            isExistData.SetSeal(request.Seal);
            isExistData.SetTemp(request.Temp);
            isExistData.SetCarNumber(request.CarNumber);
            isExistData.SetContainer(request.Container);
            isExistData.SetDoor(request.Door);
            isExistData.SetDeliver(request.Deliver);
            isExistData.SetVeterinary(request.Veterinary);
            isExistData.SetCont(request.Cont);
            isExistData.SetNote(request.Note);
            isExistData.SetOrtherNote(request.OrtherNote);
            isExistData.SetFileAttach(request.FileAttach);
            isExistData.SetNumberCode(request.NumberCode);
            isExistData.SetInvoiceNumber(request.InvoiceNumber);
            isExistData.SetTimeStart(request.TimeStart);
            isExistData.SetTimeEnd(request.TimeEnd);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            if(request.UpdateWareHouseIns.Count > 0) {
                var existingDetail = await _wareHouseInDetailRepository.Get(x => x.IdWareHouseIn==request.Id).ToListAsync(cancellationToken).ConfigureAwait(false);
                _wareHouseInDetailRepository.DeleteRange(existingDetail);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                List<WareHouseInDetail> lstDetail = new List<WareHouseInDetail>();
                foreach (var item in request.UpdateWareHouseIns)
                {
                    foreach (var item2 in item.VehicleDetails)
                    {
                        var createDetail = new WareHouseInDetail(
                             isExistData.Id,
                             item.RangeOfVehicle,
                             null,
                             item2.ProductId,
                             item2.QuantityProduct,
                             item2.Unit,
                             item2.Size,
                             item2.Weight,
                             item2.GuildId
                         );
                        lstDetail.Add(createDetail);
                    }
                }
                _wareHouseInDetailRepository.AddRange(lstDetail);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            
            methodResult.Result = _mapper.Map<UpdateWareHouseInCommandResponse>(request);
            return methodResult;
        }
    }
}