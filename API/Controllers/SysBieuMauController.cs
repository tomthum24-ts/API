using API.APPLICATION.Commands.SysBieuMau;
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
    public class SysBieuMauController : ControllerBase
    {
        private const string UploadFile = nameof(UploadFile);
        private const string UploadFileV2 = nameof(UploadFileV2);

        private readonly IMediator _mediator;

        public SysBieuMauController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tạo - Cập nhật biểu mẫu
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<CreateOrUpdateSysBieuMauCoQuanCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.AuthorizedUsers)]
        public async Task<IActionResult> UpdateSysBieuMauCoQuanAsync([FromForm] CreateOrUpdateSysBieuMauCoQuanCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Xóa biểu mẫu
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(MethodResult<DeleteSysBieuMauCoQuanCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.AuthorizedUsers)]
        public async Task<IActionResult> DeleteSysBieuMauCoQuanAsync(DeleteSysBieuMauCoQuanCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
    }
}