using API.Commands;
using API.DTOs;
using API.Queries;
using API.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using API.Common;
using System.Net;
using System.Threading.Tasks;
using API.Models;
using API.InterFace.User;

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
        [HttpGet]
        [Route(GetById)]
        public async Task<ActionResult> GetUserById(int id)
        {
            var query = await _iUser.GetInfoUserByID(id).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<UserViewModel>(query);
            return Ok(query);
        }
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
