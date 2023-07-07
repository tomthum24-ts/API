
using API.APPLICATION.Commands.RolePermission.Credential;
using API.DOMAIN;
using AutoMapper;
using BaseCommon.Attributes;
using BaseCommon.Common.MethodResult;
using BaseCommon.Common.Response;
using BaseCommon.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
namespace API.Credential
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CredentialController : ControllerBase
    {
        private const string GetById = nameof(GetById);
        private const string GetList = nameof(GetList);
        //private readonly ICredentialQueries _queires;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CredentialController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            //_queires = queires;
            _mapper = mapper;
        }
        /// <summary>
        /// Create a new Credential.
        /// </summary>
        /// <param name="command>"</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateCredentialCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCredentialAsync(CreateCredentialCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Update a existing Credential.
        /// </summary>
        /// <param name="command>"</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<UpdateCredentialCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateCredentialAsync(UpdateCredentialCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Delete a existing Credential.
        /// </summary>
        /// <param name="command>"</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(MethodResult<DeleteCredentialCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteCredentialAsync(DeleteCredentialCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        ///// <summary>
        ///// Get Credential by id.
        ///// </summary>
        ///// <param name="param>"</param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route(GetById)]
        //[ProducesResponseType(typeof(MethodResult<CredentialViewModel>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> GetCredentialByIdAsync(GetByIdParam param)
        //{
        //    var result = await _queires.GetCredentialByIdAsync(param);
        //    return Ok(result);
        //}

        ///// <summary>
        ///// Get list Credential.
        ///// </summary>
        ///// <param name="param>"</param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route(GetList)]
        //[ProducesResponseType(typeof(MethodResult<PagingItems<CredentialViewModel>>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> GetDataListAsync(GetListParam param)
        //{
        //    var result = await _queires.GetDataListAsync(param);
        //    return Ok(result);
        //}

    }
}

