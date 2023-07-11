using BaseCommon.Model;
using Microsoft.AspNetCore.Http;
using Shyjus.BrowserDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common.HttpDetection
{
    public static class HttpRequestExtensions
    {
        public static DeviceModel GetDeviceInformation(this HttpRequest request, IBrowser browserDetector)
        {
            return new DeviceModel(request, browserDetector);
        }
    }
}
