using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Common
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
