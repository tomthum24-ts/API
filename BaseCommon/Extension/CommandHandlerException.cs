using BaseCommon.Common.MethodResult;
using System.Collections.Generic;
using NetHttpStatusCode = System.Net.HttpStatusCode;
namespace API.Extension
{
    public class CommandHandlerException : BaseException
    {
        public CommandHandlerException(ErrorResult errorResult, int httpStatusCode = (int)NetHttpStatusCode.BadRequest) : base(errorResult, httpStatusCode)
        {
        }

        public CommandHandlerException(IReadOnlyCollection<ErrorResult> errorResultList, int httpStatusCode = (int)NetHttpStatusCode.BadRequest) : base(errorResultList, httpStatusCode)
        {
        }
    }
}
