using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.WareHouseOutDetail
{
    public class WareHouseOutDetailResponseViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime? DateCode { get; set; }
        public string Representative { get; set; }
        public DateTime? IntendTime { get; set; }
        public string WareHouseName { get; set; }
        public string Note { get; set; }
        public string OrtherNote { get; set; }
        public int? FileAttach { get; set; }
        public int? CreatedById { get; set; }
        public string CreateUser { get; set; }
        public string CustomerName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int? FileId { get; set; }
        public string Seal { get; set; }
        public string Temp { get; set; }
        public string CarNumber { get; set; }
        public string Container { get; set; }
        public string Door { get; set; }
        public string Deliver { get; set; }
        public string Veterinary { get; set; }
        public decimal? Cont { get; set; }
        public string Note1 { get; set; }
        public string OrtherNote1 { get; set; }
        public int? FileAttach1 { get; set; }
        public string NumberCode { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }

        public IEnumerable<WareHouseOutDetailResponseDTO> WareHouseOutDetailResponseDTOs { get; set; }
    }

    public class WareHouseOutDetailResponseDTO
    {
        public int Id { get; set; }
        public int? IdWareHouseOut { get; set; }
        public int? RangeOfVehicle { get; set; }
        public decimal? QuantityVehicle { get; set; }
        public int? ProductId { get; set; }
        public decimal? QuantityProduct { get; set; }
        public int? Unit { get; set; }
        public decimal? Size { get; set; }
        public decimal? Weight { get; set; }
        public string GuildId { get; set; }
    }
}
