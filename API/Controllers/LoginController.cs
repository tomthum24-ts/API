using API.APPLICATION.Commands.Login;
using API.APPLICATION.Commands.RefreshTooken;
using BaseCommon.Common.MethodResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private const string Login = nameof(Login);
        private const string RefreshToken = nameof(RefreshToken);
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gen token login - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<LoginCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        [Route(Login)]
        public async Task<IActionResult> LoginAsync(LoginCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);

        }

        /// <summary>
        /// RefreshToken token  - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<LoginCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        [Route(RefreshToken)]
        public async Task<IActionResult> RefreshTokenAsync(UpdateRefreshTookenCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);

        }

    }
}
