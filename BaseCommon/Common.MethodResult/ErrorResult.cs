using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseCommon.Common.MethodResult
{
    public class ErrorResult
    {
        public ErrorResult()
        {
            ErrorValues = new List<string>();
        }

        [JsonPropertyName("errorCode")]
        public string ErrorCode { get; set; }

        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("errorValues")]
        public List<string> ErrorValues { get; set; }
    }
}
