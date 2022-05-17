using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BaseCommon.Common.MethodResult
{
    public class WarningResult
    {
        public WarningResult()
        {
            WarningValues = new List<string>();
        }

        [JsonPropertyName("warningCode")]
        public string WarningCode { get; set; }

        [JsonPropertyName("warningMessage")]
        public string WarningMessage { get; set; }

        [JsonPropertyName("warningValues")]
        public List<string> WarningValues { get; set; }
    }
}
