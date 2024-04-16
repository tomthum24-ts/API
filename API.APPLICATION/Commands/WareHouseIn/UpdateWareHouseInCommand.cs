using Aspose.Words.Lists;
using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.APPLICATION.Commands.WareHouseIn
{
    public class UpdateWareHouseInCommand : IRequest<MethodResult<UpdateWareHouseInCommandResponse>>
    {
        public int Id { get; set; }
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
        public string AddressCustomer { get; set; }
        public List<UpdateWareHouseInCommandDTO> UpdateWareHouseIns { get; set; }
        public List<UpdateWareHouseInFileAttachs> WareHouseInFileAttachs { get; set; }

    }

    public class UpdateWareHouseInCommandDTO
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int? IdWareHouseIn { get; set; }
        public int? RangeOfVehicle { get; set; }
        public string ContainerNumber { get; set; }
        public string VehicleNumber { get; set; }
        public string GuildId { get; set; }
        public List<UpdateWareHouseInVehicleCommandDTO> VehicleDetails { get; set; }
    }
    public class UpdateWareHouseInVehicleCommandDTO
    {
        public int? ProductId { get; set; }
        public decimal? QuantityProduct { get; set; }
        public string ProductName { get; set; }
        public int? Unit { get; set; }
        public string UnitName { get; set; }
        public string Size { get; set; }
        public decimal? Weight { get; set; }
        //public string GuildId { get; set; }
        public string LotNo { get; set; }
        public string TotalWeighScan { get; set; }
        public string ProductDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Note { get; set; }
        public string MadeIn { get; set; }

    }
    public class UpdateWareHouseInFileAttachs
    {
        public string Name { get; set; }
        public string Path { get; set; }

    }
    public class UpdateWareHouseInCommandResponse : UpdateWareHouseInCommand
    {
    }
}