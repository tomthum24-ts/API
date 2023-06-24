using Microsoft.AspNetCore.Http;

namespace BaseCommon.Utilities
{
    public class GetInfoHelpers
    {
        private IHttpContextAccessor _accessor;

        public GetInfoHelpers(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string IpAddress()
        {
            // get source ip address for the current request
            if (_accessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                return _accessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString();
            else
                return _accessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
