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

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private const string GetListUser = nameof(GetListUser);
        private const string GetById = nameof(GetById);
        private const string Create = nameof(Create);

        private readonly IUserRepositories _iUserRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserController(IUserRepositories repository, IMapper mapper, IMediator mediator)
        {
            _iUserRepository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        [Route(GetListUser)]
        public async Task<ActionResult> GetUser( )
        {
            var query = new IUserQueries();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        [Route(GetById)]
        public ActionResult<UserReadDTO> GetUserByID( int id)
        {
            var getUser= _iUserRepository.GetById(id);
            if(getUser!=null)
            {
                return Ok(_mapper.Map<UserReadDTO>(getUser));
            }
            return NotFound();
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
