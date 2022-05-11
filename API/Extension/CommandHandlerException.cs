using API.Common;
using System.Collections.Generic;
using NetHttpStatusCode = System.Net.HttpStatusCode;
namespace API.Extension
{
    public class CommandHandlerException : BaseException
    {
        public CommandHandlerException(IReadOnlyCollection<ErrorResult> errorResultList, int httpStatusCode = (int)NetHttpStatusCode.BadRequest) : base(errorResultList, httpStatusCode)
        {
        }
    }
}
