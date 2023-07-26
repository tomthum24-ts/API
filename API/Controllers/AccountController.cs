using API.APPLICATION.Commands.Login;
using API.APPLICATION.Commands.RefreshToken;
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
    public class AccountController : ControllerBase
    {
        private const string Login = nameof(Login);
        private const string RefreshToken = nameof(RefreshToken);
        private const string Revoke = nameof(Revoke);
        private const string Logout = nameof(Logout);
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
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
        public async Task<IActionResult> RefreshTokenAsync(UpdateRefreshTokenCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);

        }

        /// <summary>
        /// Revoke token  - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<RevokeTokenCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        [Route(Revoke)]
        public async Task<IActionResult> RevokeTokenAsync(RevokeTokenCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);

        }
        /// <summary>
        /// Revoketoken  - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<RevokeTokenCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        [Route(Logout)]
        public async Task<IActionResult> LogOutAsync(RevokeTokenCommand command)
        {
            command.IsLogout = true;
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);

        }

    }
}
