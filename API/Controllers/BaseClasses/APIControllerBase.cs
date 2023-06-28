using BaseCommon.Common.MethodResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers.BaseClasses
{
    [ApiVersion("1")]
    [Route(Settings.CommandAPIDefaultRoute)]
    [ApiController]
    [Authorize]
    public class APIControllerBase :  ControllerBase
    {
        protected readonly IMediator _mediator;

        public APIControllerBase(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    }
}
