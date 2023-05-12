using BaseCommon.Common.MethodResult;
using System.Collections.Generic;
using NetHttpStatusCode = System.Net.HttpStatusCode;
namespace BaseCommon.ExceptionsModels
{
    public class ServiceException : BaseException
    {
        public ServiceException(ErrorResult errorResult, int httpStatusCode = (int)NetHttpStatusCode.BadRequest) : base(errorResult, httpStatusCode)
        {
        }

        public ServiceException(IReadOnlyCollection<ErrorResult> errorResultList, int httpStatusCode = (int)NetHttpStatusCode.BadRequest) : base(errorResultList, httpStatusCode)
        {
        }
    }
}
