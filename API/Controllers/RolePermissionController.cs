using API.APPLICATION;
using API.APPLICATION.Commands.RolePermission.Role;
using API.APPLICATION.Queries.Permission.RolePermission;
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

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RolePermissionController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRolePermissonServices _rolePermissonServices;

        public RolePermissionController(IMediator mediator, IMapper mapper, IRolePermissonServices rolePermissonServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _rolePermissonServices = rolePermissonServices;
        }

        /// <summary>
        /// Create a new UserRolePermission.
        /// </summary>
        /// <param name="command>"</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateRolePermissionCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> CreateUserRolePermissionAsync(CreateRolePermissionCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Update a existing UserRolePermission.
        /// </summary>
        /// <param name="command>"</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<UpdateRoleCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> UpdateUserRolePermissionAsync(UpdateRoleCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Delete a existing UserRolePermission.
        /// </summary>
        /// <param name="command>"</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(MethodResult<DeleteRoleCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> DeleteUserRolePermissionAsync(DeleteRoleCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// GetList RolePermission - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetList)]
        [SQLInjectionCheckOperation]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<ActionResult> GetDanhSachUserRolePermissionAsync(RolePermissionRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<RolePermissionResponseViewModel>>();

            DanhMucFilterParam danhMucFilterParam = new DanhMucFilterParam();
            danhMucFilterParam = _mapper.Map<DanhMucFilterParam>(request);
            danhMucFilterParam.TableName = TableConstants.ROLEPERMISSION_TABLENAME;

            var queryResult = await _rolePermissonServices.GetDanhMucByListIdAsync(danhMucFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<RolePermissionResponseViewModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<RolePermissionResponseViewModel>>(queryResult.Items)
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
        public async Task<IActionResult> GetUserRolePermissionByIdAsync(RolePermissionByIdRequestViewModel request)
        {
            var methodResult = new MethodResult<RolePermissionResponseViewModel>();
            var queryResult = await _rolePermissonServices.GetDanhMucByIdAsync(request.Id, TableConstants.ROLEPERMISSION_TABLENAME).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<RolePermissionResponseViewModel>(queryResult.Items.FirstOrDefault());
            return Ok(methodResult);
        }
    }
}