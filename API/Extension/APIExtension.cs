using API.Common;
using System;
using System.Linq;

namespace API.Extension
{
    public static class APIExtension
    {
        public static void AddAPIErrorMessage(this VoidMethodResult errorResult, string errorCode, string[] errorValues)
        {
            errorResult.AddErrorMessage(errorCode, GetErrorMessage(errorCode), errorValues);
        }

        public static string GetErrorMessage(string errorCode)
        {
            return ErrorHelpers.GetErrorMessage(errorCode, typeof(APIExtension).Assembly);
        }

        public static string GetColumnTableName(Type type)
        {
            var properties = type.GetProperties();
            return string.Join(",", properties.Select(x => x.Name));
        }
    }
}
