using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using API.APPLICATION.Commands.User;
using BaseCommon.Common.MethodResult;
using API.HRM.DOMAIN.DTOs.User;
using API.APPLICATION.Parameters.User;
using API.INFRASTRUCTURE;
using API.APPLICATION.ViewModels.User;
using BaseCommon.Common.Response;
using System.Collections.Generic;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private const string GetListUser = nameof(GetListUser);
        private const string GetById = nameof(GetById);
        private const string ChangePassword = nameof(ChangePassword); 
        private const string Login = nameof(Login);
     

        private readonly IUserService _iUserQueries;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IJWTManagerRepository _jWTManager;

        public UserController(IUserService repository, IMapper mapper, IMediator mediator, IJWTManagerRepository jWTManager)
        {
            _iUserQueries = repository;
            _mapper = mapper;
            _mediator = mediator;
            _jWTManager = jWTManager;
        }
        /// <summary>
        /// Get token - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route(Login)]
        public IActionResult Authenticate(Users usersdata)
        {
            var token = _jWTManager.GenerateJWTTokens(usersdata);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
        /// <summary>
        /// Get info user - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetById)]
        
        public async Task<ActionResult> GetUserByIdAsync(GetUserByIdParam param)
        {
            var query = await _iUserQueries.GetInfoUserByID(param.id).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<UserViewModel>(query);
            return Ok(query);
        }
        /// <summary>
        /// Get info user - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetListUser)]

        public async Task<ActionResult> GetDanhSachUserAsync(UserRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<UserResponseViewModel>>(); 
            var userFilterParam = _mapper.Map<UserFilterParam>(request);
            var queryResult = await _iUserQueries.GetAllUserPaging(userFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<UserResponseViewModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<UserResponseViewModel>>(queryResult.Items)
            };
            return Ok(methodResult);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateUserCommand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        
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
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
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
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        [Route(ChangePassword)]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCommand command)
        {
               var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

    }
}
