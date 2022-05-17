

namespace BaseCommon.Common.MethodResult
{
    public class Settings
    {
        public const string WarningsFileName = "WarningMessages";
        public const string NotificationMessagesFileName = "NotificationMessage";
        public const string ErrorsFileName = "ErrorMessages";
        public const string ResourceFolderName = "Resources";
        public const string TemplateFolderName = "Templates";
        public const string ForgotPassword = "forgotpassword";

        public const string CommonErrorPrefix = "ERR_COM_";
        public const int PageSizeMax = 10000;
        public const int DefaultPageSize = 10;

        // Url
        public const string APIQueryRoute = "api/[controller]";
        public const string APIDefaultRoute = "api/v{version:apiVersion}/[controller]";
        public const string CommandAPIDefaultRoute = "api/cmd/v{version:apiVersion}/[controller]";
        public const string ReadAPIDefaultRoute = "api/read/v{version:apiVersion}/[controller]";
        public const string AggregatorAPIDefaultRoute = "api/aggr/v{version:apiVersion}/[controller]";

        // Jwt Default Setting.
        public const int ValidFor = 120;
        public const string DefaultIssuer = "API.Identity";
        public const string DefaultAudience = "Everyone";
        public const int RefreshTokenExpiryTime = 2;
        public const string SecretKey = "superSecretKey@345";

        // Default Section Name.
        public const string SQLServerOptions = "SQLServerOptions";
        public const string DapperOptions = "DapperOptions";
        public const string Hangfire = "Hangfire";
        public const string DistributedCache = "DistributedCache";
        public const string JwtToken = "JwtToken";
        public const string MediaConfig = "MediaConfig";
        public const string AuditOptions = "AuditOptions";

        // Sender System Notify 
        public const int SenderSystemNotify = 0;

        // Logger
        public const string BieuMauEventType = "BieuMauEventType";
        public const string EfContextEventType = "EfContextEventType";
    }
}
