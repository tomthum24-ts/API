using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using API.APPLICATION.Commands.User;
using BaseCommon.Common.MethodResult;
using API.DOMAIN.DTOs.User;
using API.APPLICATION.Parameters.User;
using API.INFRASTRUCTURE;
using API.APPLICATION.ViewModels.User;
using BaseCommon.Common.Response;
using System.Collections.Generic;
using System.Threading;
using API.APPLICATION.ViewModels;
using System.Linq;
using BaseCommon.Utilities;
using API.APPLICATION.ViewModels.ByIdViewModel;
using BaseCommon.Attributes;
using BaseCommon.Model;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private const string GetListUser = nameof(GetListUser);
        private const string GetById = nameof(GetById);
        private const string GetPersionInfo = nameof(GetPersionInfo);
        private const string ChangePassword = nameof(ChangePassword); 
        private const string Login = nameof(Login);
        private const string Report = nameof(Report);

        private readonly IUserServices _iUserQueries;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
       

        public UserController(IUserServices repository, IMapper mapper, IMediator mediator)
        {
            _iUserQueries = repository;
            _mapper = mapper;
            _mediator = mediator;
            
        }
        /// <summary>
        /// Get info user - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetListUser)]
        [SQLInjectionCheckOperation]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<ActionResult> GetDanhSachUserAsync(UserRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<UserResponseViewModel>>();
            var userFilterParam = _mapper.Map<UserFilterParam>(request);
            var queryResult = await _iUserQueries.GetUserPagingAsync(userFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<UserResponseViewModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<UserResponseViewModel>>(queryResult.Items)
            };
            return Ok(methodResult);
        }
        [HttpPost]
        [Route(GetById)]
        [ProducesResponseType(typeof(MethodResult<ResponseByIdViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [SQLInjectionCheckOperation]
        [AuthorizeGroupCheckOperation(EAuthorizeType.Everyone)]
        public async Task<ActionResult> GetUserByIdAsync(UserRequestByIdModel param)
        {
            var methodResult = new MethodResult<UserResponseByIdModel>();
            var userFilterParam = _mapper.Map<GetUserByIdParam>(param);
            var query = await _iUserQueries.GetInfoUserByIdAsync(userFilterParam).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UserResponseByIdModel>(query);
            return Ok(methodResult);
        }
        [HttpPost]
        [Route(GetPersionInfo)]
        [ProducesResponseType(typeof(MethodResult<ResponseByIdViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [SQLInjectionCheckOperation]
        [AuthorizeGroupCheckOperation(EAuthorizeType.Everyone)]
        public async Task<ActionResult> GetInfoPersionalByIdAsync()
        {
            var methodResult = new MethodResult<UserResponseByIdModel>();
            var query = await _iUserQueries.GetInfoPersionalById().ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UserResponseByIdModel>(query);
            return Ok(methodResult);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateUserCommand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> CreateUserAsync(CreateUserCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
        /// <summary>
        /// Delete user - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(MethodResult<DeleteUserCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> DeleteUserAsync(DeleteUserCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
        /// <summary>
        /// Update User- (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<UpdateUserCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
        /// <summary>
        /// Change password User- (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<ChangePasswordCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [Route(ChangePassword)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
        /// <summary>
        /// Report test
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[AuthorizeGroupCheckOperation(EAuthorizeType.AuthorizedUsers)]
        [HttpPost(Report)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> ExportThongTinAsync(RequestByIdViewModel request)
        {

            var result = await _iUserQueries.ExportWordThongTinAsync(request).ConfigureAwait(false);

            return File(result.OutputStream, result.ContentType, result.TenBieuMau);
        }
    }
}
