using API.APPLICATION.ViewModels.Security;
using BaseCommon.Common.MethodResult;
using BaseCommon.Common.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecurityController  : ControllerBase
    {
        private readonly IMediator _mediator;
        private const string Decrypt = nameof(Decrypt);
        private const string Encrypt = nameof(Encrypt);

        public SecurityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Decrypt - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(Decrypt)]
        public ActionResult DecryptAsync(SecurityViewModel request)
        {
           var key=  SecurityHelper.Decrypt(request.Value, request.Password);
            return Ok(key);
        }

        /// <summary>
        /// Decrypt - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(Encrypt)]
        public ActionResult EncryptAsync(SecurityViewModel request)
        {
            var key = SecurityHelper.Encrypt(request.Value, request.Password);
            return Ok(key);
        }
    }
}
