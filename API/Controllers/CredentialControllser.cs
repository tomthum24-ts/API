using API.APPLICATION;
using API.APPLICATION.Commands.RolePermission.Credential;
using API.APPLICATION.Queries;
using API.APPLICATION.ViewModels.Permission;
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
        private readonly ICredentialServices _credentialServices;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CredentialController(IMediator mediator, IMapper mapper, ICredentialServices credentialServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _credentialServices = credentialServices;
        }

        /// <summary>
        /// Create a new Credential.
        /// </summary>
        /// <param name="command>"</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateCredentialCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
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
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
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
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> DeleteCredentialAsync(DeleteCredentialCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// GetList GroupPermission - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetList)]
        [SQLInjectionCheckOperation]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<ActionResult> GetDanhSachAsync(CredentialRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<CredentialResponseViewModel>>();

            DanhMucFilterParam danhMucFilterParam = new DanhMucFilterParam();
            danhMucFilterParam = _mapper.Map<DanhMucFilterParam>(request);
            danhMucFilterParam.TableName = TableConstants.CREDENTIAL_TABLENAME;

            var queryResult = await _credentialServices.GetDanhMucByListIdAsync(danhMucFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<CredentialResponseViewModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<CredentialResponseViewModel>>(queryResult.Items)
            };
            return Ok(methodResult);
        }

        /// <summary>
        /// Get List of GetUserRolePermissionById.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(GetById)]
        [ProducesResponseType(typeof(MethodResult<RolePermissionResponseViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [SQLInjectionCheckOperation]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> GetUserRolePermissionByIdAsync(redentialByIdRequestViewModel request)
        {
            var methodResult = new MethodResult<CredentialResponseViewModel>();
            var queryResult = await _credentialServices.GetDanhMucByIdAsync(request.Id, TableConstants.CREDENTIAL_TABLENAME).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CredentialResponseViewModel>(queryResult.Items.FirstOrDefault());
            return Ok(methodResult);
        }
    }
}