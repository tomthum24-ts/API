using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.WareHouseOut
{
    public class UpdateWareHouseOutCommand : IRequest<MethodResult<UpdateWareHouseOutCommandResponse>>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime? DateCode { get; set; }
        public int? CustomerID { get; set; }
        public string Representative { get; set; }
        public DateTime? IntendTime { get; set; }
        public int? WareHouse { get; set; }
        public string Note { get; set; }
        public string OrtherNote { get; set; }
        public int? FileAttach { get; set; }
        public List<UpdateWareHouseOutCommandDTO> UpdateWareHouseOuts { get; set; }

    }

    public class UpdateWareHouseOutCommandDTO
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int IdWareHouseOut { get; set; }
        public int RangeOfVehicle { get; set; }
        public decimal QuantityVehicle { get; set; }
        public int ProductId { get; set; }
        public decimal QuantityProduct { get; set; }
        public int Unit { get; set; }
        public decimal Size { get; set; }
        public decimal Weight { get; set; }
        public bool IsDelete { get; set; }
        public string RONumber { get; set; }
    }

    public class UpdateWareHouseOutCommandResponse : UpdateWareHouseOutCommand
    {
    }
}
