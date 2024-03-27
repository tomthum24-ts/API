﻿using System;
using System.Collections.Generic;

namespace API.APPLICATION.ViewModels.WareHouseInDetail
{
    public class WareHouseInDetailViewModel
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

        public string NumberCode { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public string Pallet { get; set; }
        public IEnumerable<WareHouseInDetailModel> WareHouseInDetailModels { get; set; }
    }

    public class WareHouseInDetailModel
    {
        public int Id { get; set; }
        //public int? IdWareHouseIn { get; set; }
        public int? RangeOfVehicle { get; set; }
        public string GuildId { get; set; }
        public IEnumerable<VehicleDetaiModel> VehicleDetaiModels { get; set; }
    }
    public class VehicleDetaiModel
    {
        //public decimal? QuantityVehicle { get; set; }
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
        
    }
}