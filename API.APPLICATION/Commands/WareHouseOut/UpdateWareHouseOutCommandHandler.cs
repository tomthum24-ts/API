using API.APPLICATION.Commands.WareHouseOut;
using API.DOMAIN;
using API.DOMAIN.DomainObjects.WareHouseOutDetail;
using API.DOMAIN.DomainObjects.WareHouseOutDetail;
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

namespace API.APPLICATION.Commands.WareHouseOut
{
    public class UpdateWareHouseOutCommandHandler : IRequestHandler<UpdateWareHouseOutCommand, MethodResult<UpdateWareHouseOutCommandResponse>>
    {
        private readonly IWareHouseOutRepository _WareHouseOutRepository;
        private readonly IWareHouseOutDetailRepository _WareHouseOutDetailRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWareHouseOutCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWareHouseOutRepository WareHouseOutRepository, IWareHouseOutDetailRepository WareHouseOutDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _WareHouseOutRepository = WareHouseOutRepository;
            _WareHouseOutDetailRepository = WareHouseOutDetailRepository;
        }

        public async Task<MethodResult<UpdateWareHouseOutCommandResponse>> Handle(UpdateWareHouseOutCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateWareHouseOutCommandResponse>();
            var isExistData = await _WareHouseOutRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
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
            if (request.UpdateWareHouseOuts.Count > 0)
            {
                var existingDetail = await _WareHouseOutDetailRepository.Get(x => x.IdWareHouseOut == request.Id).ToListAsync(cancellationToken).ConfigureAwait(false);
                _WareHouseOutDetailRepository.DeleteRange(existingDetail);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                List<WareHouseOutDetail> lstDetail = new List<WareHouseOutDetail>();
                foreach (var item in request.UpdateWareHouseOuts)
                {
                    foreach (var item2 in item.VehicleDetails)
                    {
                        var createDetail = new WareHouseOutDetail(
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
                _WareHouseOutDetailRepository.AddRange(lstDetail);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

            methodResult.Result = _mapper.Map<UpdateWareHouseOutCommandResponse>(request);
            return methodResult;
        }
    }
}