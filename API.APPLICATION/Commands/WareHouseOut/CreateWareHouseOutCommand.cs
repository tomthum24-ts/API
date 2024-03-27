using BaseCommon.Common.MethodResult;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace API.APPLICATION.Commands.WareHouseOut
{
    public class CreateWareHouseOutCommand : IRequest<MethodResult<CreateWareHouseOutCommandResponse>>
    {
        public string Code { get; set; }
        public DateTime? DateCode { get; set; }
        public int? CustomerID { get; set; }
        public string Representative { get; set; }
        public DateTime? IntendTime { get; set; }
        public int? WareHouse { get; set; }
        public string CustomerName { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Seal { get; set; }
        public string Temp { get; set; }
        public string CarNumber { get; set; }
        public string Container { get; set; }
        public string Door { get; set; }
        public string Deliver { get; set; }
        public string Veterinary { get; set; }
        public string Cont { get; set; }
        public string Note { get; set; }
        public string OrtherNote { get; set; }
        public int? FileAttach { get; set; }
        public string NumberCode { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public string Pallet { get; set; }
        public List<CreateWareHouseOutDetailCommandDTO> WareHouseOutDetail { get; set; }
    }

    public class CreateWareHouseOutDetailCommandDTO
    {
        [JsonIgnore]
        public int? IdWareHouseOut { get; set; }

        public int? RangeOfVehicle { get; set; }
        public string GuildId { get; set; }
        public List<CreateWareHouseOutVehicleCommandDTO> VehicleDetails { get; set; }
    }

    public class CreateWareHouseOutVehicleCommandDTO
    {
        // public decimal? QuantityVehicle { get; set; }
        public int? ProductId { get; set; }

        public decimal? QuantityProduct { get; set; }
        public int? Unit { get; set; }
        public decimal? Size { get; set; }
        public decimal? Weight { get; set; }
        public string Note { get; set; }
        public string LotNo { get; set; }
        public string TotalWeighScan { get; set; }
        public string ProductDate { get; set; }
        public string ExpiryDate { get; set; }
        public string MadeIn { get; set; }
        public string GuildId { get; set; }
        public string RONumber { get; set; }
    }

    public class CreateWareHouseOutCommandResponse : CreateWareHouseOutCommand
    {
    }
}