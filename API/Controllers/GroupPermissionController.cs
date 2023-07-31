using API.APPLICATION;
using API.APPLICATION.Commands.GroupPermission;
using API.APPLICATION.Queries.GroupPermission;
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
    public class GroupPermissionController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserGroupPermissionServices _userGroupPermissionServices;

        public GroupPermissionController(IMediator mediator, IMapper mapper, IUserGroupPermissionServices userGroupPermissionServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userGroupPermissionServices = userGroupPermissionServices;
        }

        /// <summary>
        /// Create a new UserGroupPermission.
        /// </summary>
        /// <param name="command>"</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateGroupPermissionCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> CreateUserGroupPermissionAsync(CreateGroupPermissionCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Update a existing UserGroupPermission.
        /// </summary>
        /// <param name="command>"</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<UpdateGroupPermissionCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> UpdateUserGroupPermissionAsync(UpdateGroupPermissionCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Delete a existing UserGroupPermission.
        /// </summary>
        /// <param name="command>"</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(MethodResult<DeleteGroupPermissionCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> DeleteUserGroupPermissionAsync(DeleteGroupPermissionCommand command)
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
        public async Task<ActionResult> GetDanhSachUserGroupPermissionAsync(UserGroupPermissionRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<UserGroupPermissionResponseViewModel>>();

            DanhMucFilterParam danhMucFilterParam = new DanhMucFilterParam();
            danhMucFilterParam = _mapper.Map<DanhMucFilterParam>(request);
            danhMucFilterParam.TableName = TableConstants.USERPERMISSION_TABLENAME;

            var queryResult = await _userGroupPermissionServices.GetDanhMucByListIdAsync(danhMucFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<UserGroupPermissionResponseViewModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<UserGroupPermissionResponseViewModel>>(queryResult.Items)
            };
            return Ok(methodResult);
        }

        /// <summary>
        /// Get List of GetUserGroupPermissionById.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(GetById)]
        [ProducesResponseType(typeof(MethodResult<UserGroupPermissionResponseViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [SQLInjectionCheckOperation]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> GetUserGroupPermissionByIdAsync(UserGroupPermissionByIdRequestViewModel request)
        {
            var methodResult = new MethodResult<UserGroupPermissionResponseViewModel>();
            var queryResult = await _userGroupPermissionServices.GetDanhMucByIdAsync(request.Id, TableConstants.USERPERMISSION_TABLENAME).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UserGroupPermissionResponseViewModel>(queryResult.Items.FirstOrDefault());
            return Ok(methodResult);
        }
    }
}