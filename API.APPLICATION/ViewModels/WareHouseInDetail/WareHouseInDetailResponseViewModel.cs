using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.WareHouseInDetail
{
    public class WareHouseInDetailResponseViewModel
    {
        public IEnumerable<WareHouseInResponseDTO> WareHouseInResponseDTOs { get; set; }

        public IEnumerable<WareHouseInDetailResponseDTO> WareHouseInDetailResponseDTOs { get; set; } 
    }
    public class WareHouseInResponseDTO
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
        public string Cont { get; set; }
        public string Note1 { get; set; }
        public string OrtherNote1 { get; set; }
        public int? FileAttach1 { get; set; }
        public string NumberCode { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public string Pallet { get; set; }
    }
    public class WareHouseInDetailResponseDTO
    {
        public long? STT { get; set; }
        public int Id { get; set; }
        public int? IdWareHouseIn { get; set; }
        public int? RangeOfVehicle { get; set; }
        public decimal? QuantityVehicle { get; set; }
        public int? ProductId { get; set; }
        public decimal? QuantityProduct { get; set; }
        public int? Unit { get; set; }
        public decimal? Size { get; set; }
        public decimal? Weight { get; set; }
        public string GuildId { get; set; }
        public string DonViTinh { get; set; }
        public string TenLoaiXe { get; set; }
        public string TenSp { get; set; }
        public string MaSp { get; set; }
        public string Note { get; set; }
        public decimal? TotalWeigh { get; set; }
        public string LotNo { get; set; }
        public string TotalWeighScan { get; set; }
        public string ProductDate { get; set; }
        public string ExpiryDate { get; set; }
        public string MadeIn { get; set; }
    }
}