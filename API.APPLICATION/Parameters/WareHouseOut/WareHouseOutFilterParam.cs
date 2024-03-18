using System;
using System.Text.Json.Serialization;

namespace API.APPLICATION.Parameters.WareHouseOut
{
    public class WareHouseOutFilterParam : PagingDTO
    {
        [JsonIgnore]
        public int IdUser { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}