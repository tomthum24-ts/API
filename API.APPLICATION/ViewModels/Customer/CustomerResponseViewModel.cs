﻿using System;

namespace API.APPLICATION.ViewModels.Customer
{
    public class CustomerResponseViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Province { get; set; }
        public int? District { get; set; }
        public int? Village { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string CMND { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string TaxCode { get; set; }
        public int? GroupMember { get; set; }
        public int? FileAttach { get; set; }
        public bool? IsEnterprise { get; set; }

    }
}