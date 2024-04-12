using API.APPLICATION.Commands.WareHouseOut;

using API.APPLICATION.Commands.WareHouseOut;

using API.DOMAIN.DomainObjects.WareHouseOutDetail;
using API.DOMAIN;
using API.DOMAIN.DomainObjects.WareHouseOut;

using API.DOMAIN.DomainObjects.WareHouseOutDetail;

using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.APPLICATION.ViewModels.Notification;
using API.APPLICATION.Services.Notifications;
using API.INFRASTRUCTURE.Interface;
using API.DOMAIN.DomainObjects.WareHouseOutFileAttach;

namespace API.APPLICATION
{
    public class CreateWareHouseOutCommandHandler : IRequestHandler<CreateWareHouseOutCommand, MethodResult<CreateWareHouseOutCommandResponse>>
    {
        private readonly IWareHouseOutRepository _WareHouseOutRepository;
        private readonly IWareHouseOutDetailRepository _WareHouseOutDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IWareHouseOutFileAttachRepository _wareHouseOutFileAttachRepository;

        public CreateWareHouseOutCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IWareHouseOutRepository WareHouseOutRepository, IWareHouseOutDetailRepository WareHouseOutDetailRepository, INotificationService notificationService, IWareHouseOutFileAttachRepository wareHouseOutFileAttachRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _WareHouseOutRepository = WareHouseOutRepository;
            _WareHouseOutDetailRepository = WareHouseOutDetailRepository;
            _notificationService = notificationService;
            _wareHouseOutFileAttachRepository = wareHouseOutFileAttachRepository;
        }

        public async Task<MethodResult<CreateWareHouseOutCommandResponse>> Handle(CreateWareHouseOutCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateWareHouseOutCommandResponse>();
            //bool existingCode = await _WareHouseOutRepository.Get(x => x.Code == request.Code).AnyAsync(cancellationToken);
            //if (existingCode)
            //{
            //    methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
            //        {
            //            ErrorHelpers.GenerateErrorResult(nameof(request.Code), request.Code)
            //        });
            //    return methodResult;
            //}
            var createWareHouse = new WareHouseOut(
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
                    request.TimeEnd,
                    request.Pallet,
                    request.AddressCustomer
                );
            _WareHouseOutRepository.Add(createWareHouse);
            var idWareHouse = await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            List<WareHouseOutDetail> lstDetail = new List<WareHouseOutDetail>();
            if (request.WareHouseOutDetail.Count < 1)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB07), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.Code), request.Code)
                    });
                return methodResult;
            }
            foreach (var item in request.WareHouseOutDetail)
            {
                foreach (var item2 in item.VehicleDetails)
                {
                    var createDetail = new WareHouseOutDetail(
                                createWareHouse.Id,
                                item.RangeOfVehicle,
                                null,
                                item2.ProductId,
                                item2.QuantityProduct,
                                item2.Unit,
                                item2.Size,
                                item2.Weight,
                                item2.RONumber,
                                item.GuildId,
                                item2.Note,
                                item2.LotNo,
                                item2.TotalWeighScan,
                                item2.ProductDate,
                                item2.ExpiryDate,
                                item2.MadeIn,
                                item2.ProductName,
                                item2.UnitName
                            );
                    lstDetail.Add(createDetail);
                }
            }
            _WareHouseOutDetailRepository.AddRange(lstDetail);
            //var idWareHouse = await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            if(request.WareHouseOutFileAttachs.Count > 0)
            {
                List<WareHouseOutFileAttachs> WareHouseOutFileAttach = new List<WareHouseOutFileAttachs>();
                foreach (var file in request.WareHouseOutFileAttachs)
                {
                    var createFile = new WareHouseOutFileAttachs(
                            createWareHouse.Id,
                            file.Name,
                            file.Path
                        );
                    WareHouseOutFileAttach.Add(createFile);
                }
                _wareHouseOutFileAttachRepository.AddRange(WareHouseOutFileAttach);
                _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
           
            methodResult.Result = _mapper.Map<CreateWareHouseOutCommandResponse>(request);
            try
            {
                var noti = new NotificationModel();
                noti.Title = "Đơn hàng xuất mới";
                noti.Body = "Đơn hàng  " + request.Code + " vừa được tạo";
                noti.URL.ModuleName = "Xuất kho";
                noti.URL.ModuleType = (int)TypeNotifyEnum.WareHouseOut;
                noti.URL.Id = idWareHouse;
                _notificationService.SendNotiToAdmin(noti);
            }
            catch (System.Exception)
            {
            }
            return methodResult;
        }
    }
}