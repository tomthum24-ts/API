using API.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using API.Common;
using System.Net;
using System.Threading.Tasks;
using API.Models;
using API.Services;
using API.Helpers;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private const string GetListUser = nameof(GetListUser);
        private const string GetById = nameof(GetById);
        private const string Create = nameof(Create);

        private readonly IUserService _iUser;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserController(IUserService repository, IMapper mapper, IMediator mediator)
        {
            _iUser = repository;
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(MethodResult<AuthenticateRequest>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _iUser.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        [Authorize]
        [HttpGet]
        [Route(GetById)]
        
        public async Task<ActionResult> GetUserById(int id)
        {
            var query = await _iUser.GetInfoUserByID(id).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<UserViewModel>(query);
            return Ok(query);
        }
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateUserCommand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [Route(Create)]
        
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
      
    }
}
