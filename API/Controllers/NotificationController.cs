using API.APPLICATION;
using API.APPLICATION.Commands.Notification;
using API.APPLICATION.Commands.Project;
using API.APPLICATION.Services.Notifications;
using API.APPLICATION.ViewModels.Notification;
using AutoMapper;
using BaseCommon.Attributes;
using BaseCommon.Common.MethodResult;
using BaseCommon.Model;
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
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public NotificationController(INotificationService notificationService, IMapper mapper, IMediator mediator)
        {
            _notificationService = notificationService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {
            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateNotificationTokenCommand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> CreateNotificationTokenAsync(CreateNotificationTokenCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
