using BaseCommon.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common.HttpDetection
{
    public class HttpRequestHelpers
    {
        public static string GetMarkerFullInfo(HttpRequest request)
        {
            var agent = GetUserAgent(request);

            if (string.IsNullOrWhiteSpace(agent))
            {
                return null;
            }

            int iEnd = agent.IndexOf('(');

            if (iEnd < 0)
            {
                return null;
            }

            string markerFullInfo = agent.Substring(0, iEnd).Trim();

            return markerFullInfo;
        }

        public static string GetMarkerName(HttpRequest request)
        {
            string markerName = GetMarkerFullInfo(request)?.Split('/').FirstOrDefault()?.Trim();

            return markerName;
        }

        public static string GetMarkerVersion(HttpRequest request)
        {
            string markerVersion = GetMarkerFullInfo(request)?.Split('/').LastOrDefault()?.Trim();

            return markerVersion;
        }

        public static string GetOsFullInfo(HttpRequest request)
        {
            var agent = GetUserAgent(request);

            if (string.IsNullOrWhiteSpace(agent))
            {
                return null;
            }

            int iStart = agent.IndexOf('(') + 1;

            int iEnd = agent.IndexOf(')') - iStart;

            if (iEnd < 0)
            {
                return null;
            }

            string osFullInfo = agent.Substring(iStart, iEnd).Trim();

            return osFullInfo;
        }

        public static string GetOsName(HttpRequest request)
        {
            string osName = GetOsFullInfo(request)?.Split(';').FirstOrDefault()?.Trim();

            return osName;
        }

        public static string GetOsVersion(HttpRequest request)
        {
            var info = GetOsFullInfo(request)?.Split(';');

            string osVersion = null;

            if (info?.Any() != true || info.Length <= 1)
            {
                return null;
            }

            var i = 1;

            while (i <= info.Length && (osVersion == null || osVersion.ToLowerInvariant() == "u"))
            {
                osVersion = info[i];

                i++;
            }

            return osVersion;
        }

        public static string GetEngineFullInfo(HttpRequest request)
        {
            var agent = GetUserAgent(request);

            if (string.IsNullOrWhiteSpace(agent))
            {
                return null;
            }

            int iStart = agent.IndexOf(')') + 1;

            string engineFullInfo = agent.Substring(iStart).Trim();

            if (string.IsNullOrWhiteSpace(engineFullInfo))
            {
                return null;
            }

            int iEnd = engineFullInfo.IndexOf(' ');

            if (iEnd < 0)
            {
                return null;
            }

            engineFullInfo = engineFullInfo.Substring(0, iEnd);

            return engineFullInfo;
        }

        public static string GetEngineName(HttpRequest request)
        {
            string engineName = GetEngineFullInfo(request)?.Split('/').FirstOrDefault()?.Trim();

            const string webKitStandardName = "WebKit";

            engineName = engineName?.EndsWith(webKitStandardName) == true ? webKitStandardName : engineName;

            return engineName;
        }

        public static string GetEngineVersion(HttpRequest request)
        {
            string engineName = GetEngineFullInfo(request)?.Split('/').LastOrDefault()?.Trim();

            return engineName;
        }

        public static string GetUserAgent(HttpRequest request)
        {
            StringValues value = new();

            return request?.Headers.TryGetValue(HeaderKey.UserAgent, out value) == true ? value.ToString() : null;
        }
        public static string GetIpAddress(HttpRequest request)
        {
            var ipAddress = string.Empty;

            // Priority to Proxy Server

            if (request.Headers.TryGetValue(HeaderKey.CFConnectingIP, out var cloudFareConnectingIp))
            {
                ipAddress = cloudFareConnectingIp;

                return ipAddress;
            }

            if (request.Headers.TryGetValue(HeaderKey.CFTrueClientIP, out var cloudFareTrueClientIp))
            {
                ipAddress = cloudFareTrueClientIp;

                return ipAddress;
            }

            // Look for the X-Forwarded-For (XFF) HTTP header field it's used for identifying the
            // originating IP address of a client connecting to a web server through an HTTP proxy or
            // load balancer.
            string xff = request.Headers?
                .Where(x => HeaderKey.XForwardedFor.Equals(x.Key, StringComparison.OrdinalIgnoreCase))
                .Select(k => request.Headers[k.Key]).FirstOrDefault();

            // If you want to exclude private IP addresses, then see http://stackoverflow.com/questions/2577496/how-can-i-get-the-clients-ip-address-in-asp-net-mvc
            if (!string.IsNullOrWhiteSpace(xff))
            {
                var lastIp = xff.Split(',').FirstOrDefault();
                ipAddress = lastIp;
            }

            if (string.IsNullOrWhiteSpace(ipAddress) || ipAddress == "::1" || ipAddress == "127.0.0.1")
            {
                ipAddress = request.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            }

            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                return null;
            }

            // Standardize
            if (ipAddress == "::1")
            {
                ipAddress = "127.0.0.1";
            }

            // Remove port
            int index = ipAddress.IndexOf(":", StringComparison.OrdinalIgnoreCase);

            if (index > 0)
            {
                ipAddress = ipAddress.Substring(0, index);
            }

            return ipAddress;
        }

    }
}
