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
        [Route(GetById)]
        [ProducesResponseType(typeof(MethodResult<ResponseByIdViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [SQLInjectionCheckOperation]
        public async Task<ActionResult> GetUserByIdAsync(RequestByIdViewModel param, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<Dictionary<string, string>>();
            var query = await _iUserQueries.GetInfoUserByID(param).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<UserViewModel>(query);
            Dictionary<string,string> data= query.ToDictionary(x => x.ObjKey, x => StringHelpers.Normalization(x.ObjValue));
            methodResult.Result = data;
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
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

    }
}
