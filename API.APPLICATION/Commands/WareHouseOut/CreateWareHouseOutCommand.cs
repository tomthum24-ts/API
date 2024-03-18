using BaseCommon.Common.MethodResult;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace API.APPLICATION.Commands.WareHouseOut
{
    public class CreateWareHouseOutCommand : IRequest<MethodResult<CreateWareHouseOutCommandResponse>>
    {
        [JsonIgnore]
        public string Code { get; set; }

        public DateTime? DateCode { get; set; }
        public int? CustomerID { get; set; }
        public string Representative { get; set; }
        public DateTime? IntendTime { get; set; }
        public int? WareHouse { get; set; }
        public string Note { get; set; }
        public string OrtherNote { get; set; }
        public int? FileAttach { get; set; }
        public List<CreateWareHouseOutDetailCommandDTO> WareHouseOutDetail { get; set; }
    }

    public class CreateWareHouseOutDetailCommandDTO
    {
        [JsonIgnore]
        public int? IdWareHouseOut { get; set; }

        public int? RangeOfVehicle { get; set; }
        public decimal? QuantityVehicle { get; set; }
        public int? ProductId { get; set; }
        public decimal? QuantityProduct { get; set; }
        public int? Unit { get; set; }
        public decimal? Size { get; set; }
        public decimal? Weight { get; set; }
        public string RONumber { get; set; }
    }

    public class CreateWareHouseOutCommandResponse : CreateWareHouseOutCommand
    {
    }
}