using API.DOMAIN;
using API.DOMAIN.DomainObjects.WareHouseInDetail;
using API.DOMAIN.DomainObjects.WareHouseInFileAttach;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface;
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
        private readonly IWareHouseInFileAttachRepository _wareHouseInFileAttachRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWareHouseInCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWareHouseInRepository wareHouseInRepository, IWareHouseInDetailRepository wareHouseInDetailRepository, IWareHouseInFileAttachRepository wareHouseInFileAttachRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _wareHouseInRepository = wareHouseInRepository;
            _wareHouseInDetailRepository = wareHouseInDetailRepository;
            _wareHouseInFileAttachRepository = wareHouseInFileAttachRepository;
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
            if(isExistData.Step == 1)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB14), new[]
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
            isExistData.SetPallet(request.Pallet);
            isExistData.SetRequire(request.Require);
            isExistData.SetAddressCustomer(request.AddressCustomer);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            #region Detail
            var existingDetail = await _wareHouseInDetailRepository.Get(x => x.IdWareHouseIn == request.Id).ToListAsync(cancellationToken).ConfigureAwait(false);
            if(existingDetail != null)
            {
                _wareHouseInDetailRepository.DeleteRange(existingDetail);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            
            if (request.UpdateWareHouseIns.Count > 0) {
                
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
                            item.GuildId,
                            item2.Note,
                            item2.LotNo,
                            item2.TotalWeighScan,
                            item2.ProductDate,
                            item2.ExpiryDate,
                            item2.MadeIn,
                            item2.ProductName,
                            item2.UnitName,
                            item.ContainerNumber,
                            item.VehicleNumber
                         );
                        lstDetail.Add(createDetail);
                    }
                }
                _wareHouseInDetailRepository.AddRange(lstDetail);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            #endregion
            #region Upload file
            var existingFile = await _wareHouseInFileAttachRepository.Get(x => x.IdWareHouseIn == request.Id).ToListAsync(cancellationToken).ConfigureAwait(false);
            if(existingFile != null)
            {
                _wareHouseInFileAttachRepository.DeleteRange(existingFile);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
           
            if (request.WareHouseInFileAttachs.Count > 0)
            {
               
                List<WareHouseInFileAttachs> WareHouseInFileAttach = new List<WareHouseInFileAttachs>();
                foreach (var file in request.WareHouseInFileAttachs)
                {
                    var createFile = new WareHouseInFileAttachs(
                            isExistData.Id,
                            file.Name,
                            file.Path
                        );
                    WareHouseInFileAttach.Add(createFile);
                }
                _wareHouseInFileAttachRepository.AddRange(WareHouseInFileAttach);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            #endregion
            methodResult.Result = _mapper.Map<UpdateWareHouseInCommandResponse>(request);
            return methodResult;
        }
    }
}