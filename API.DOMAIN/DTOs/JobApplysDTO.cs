using System;

namespace API.DOMAIN.DTOs
{
    public class JobApplysDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdJobs { get; set; }
        public int? IdUser { get; set; }
        public string TimeApply { get; set; }
        public int? Number { get; set; }
        public string Note { get; set; }
    }
}
