using API.DOMAIN.DTOs.WareHouseIn;
using System;
using System.Collections.Generic;

namespace API.APPLICATION.ViewModels.WareHouseIn
{
    public class WareHouseInResponseViewModel
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
 
    }

}