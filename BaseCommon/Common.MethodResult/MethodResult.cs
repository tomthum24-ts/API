using System;
using System.Text.Json.Serialization;
using NewtonsoftJsonConvert = Newtonsoft.Json.JsonConvert;

namespace BaseCommon.Common.MethodResult
{
    public class MethodResult<T> : VoidMethodResult
    {
        [JsonPropertyName("result")]
        public T Result { get; set; }

        [JsonIgnore]
        public string EncryptionResult
        {
            get
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                string password = AppSettings.Instance.Get<string>("password");

                if (IsOk && Result != null && env == "Production")
                {
                    return SecurityHelper.Encrypt(NewtonsoftJsonConvert.SerializeObject(Result), password);
                }

                return null;
            }
        }
    }
}
