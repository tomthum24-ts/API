﻿using System;
using System.Collections.Generic;

namespace API.DOMAIN.DTOs.WareHouseOut
{
    public class WareHouseOutByIdDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime? DateCode { get; set; }
        public string Code1 { get; set; }
        public string CustomerName { get; set; }
        public string Representative { get; set; }
        public DateTime? IntendTime { get; set; }
        public string WareHouseName { get; set; }
        public string Note { get; set; }
        public string OrtherNote { get; set; }
        public int? FileAttach { get; set; }
        public int? CreatedById { get; set; }
        public string CreateUser { get; set; }
        public IEnumerable<WareHouseOutDetailByIdModel> WareHouseOutDetailByIdModels { get; set; }
    }

    public class WareHouseOutDetailByIdModel
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
    }
}