using API.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using API.Common;
using System.Net;
using System.Threading.Tasks;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using API.Models.ACL;
using API.Param.ACL;
using API.InterFace;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private const string GetListUser = nameof(GetListUser);
        private const string GetById = nameof(GetById);
 
        private readonly IUserService _iUser;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IJWTManagerRepository _jWTManager;

        public UserController(IUserService repository, IMapper mapper, IMediator mediator, IJWTManagerRepository jWTManager)
        {
            _iUser = repository;
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
        [Route("authenticate")]
        public IActionResult Authenticate(Users usersdata)
        {
            var token = _jWTManager.Authenticate(usersdata);
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
        
        public async Task<ActionResult> GetUserById(GetUserByIdParam param)
        {
            var query = await _iUser.GetInfoUserByID(param.id).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<UserViewModel>(query);
            return Ok(query);
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

    }
}
