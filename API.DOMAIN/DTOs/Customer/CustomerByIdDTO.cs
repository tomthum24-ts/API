using System;

namespace API.DOMAIN.DTOs.Customer
{
    public class CustomerByIdDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Province { get; set; }
        public string ProvinceName { get; set; }
        public int? District { get; set; }
        public string DistrictName { get; set; }
        public int? Village { get; set; }
        public string VillageName { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string CMND { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string TaxCode { get; set; }
        public int? GroupMember { get; set; }
        public string GroupName { get; set; }
        public int? FileAttach { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public bool? IsEnterprise { get; set; }
        public string Representative { get; set; }
        public string Poisition { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string RegistrationAddress { get; set; }
        public string UserCreate { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UserUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}