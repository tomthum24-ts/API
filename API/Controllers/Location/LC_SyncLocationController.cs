using API.APPLICATION.Commands.Location.SyncLocation;
using API.APPLICATION.Queries.Location;
using API.APPLICATION.ViewModels.Location;
using BaseCommon.Attributes;
using BaseCommon.Common.MethodResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Controllers.Location
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LC_SyncLocationController : ControllerBase
    {
        private const string GetData = nameof(GetData);
        private readonly IMediator _mediator;

        public LC_SyncLocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route(GetData)]
        [ProducesResponseType(typeof(MethodResult<SyncLocationCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AllowAnonymous]
        [SQLInjectionCheckOperation]
        public async Task<ActionResult> GetDataLocationAsync(SyncLocationCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
