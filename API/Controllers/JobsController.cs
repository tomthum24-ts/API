using API.APPLICATION.Commands.Jobs;
using AutoMapper;
using BaseCommon.Attributes;
using BaseCommon.Common.MethodResult;
using BaseCommon.Common.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        //private readonly IJobsServices _JobsServices;

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public JobsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        //[HttpPost]
        //[Route(GetList)]
        //[SQLInjectionCheckOperation]
        ////[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        //[AllowAnonymous]
        //public async Task<ActionResult> GetDanhSachJobsAsync(JobsRequestViewModel request)
        //{
        //    var methodResult = new MethodResult<PagingItems<JobsResponseViewModel>>();
        //    var userFilterParam = _mapper.Map<JobsFilterParam>(request);

        //    var queryResult = await _JobsServices.GetJobsPagingAsync(userFilterParam).ConfigureAwait(false);
        //    methodResult.Result = new PagingItems<JobsResponseViewModel>
        //    {
        //        PagingInfo = queryResult.PagingInfo,
        //        Items = _mapper.Map<IEnumerable<JobsResponseViewModel>>(queryResult.Items)
        //    };

        //    return Ok(methodResult);
        //}
        /// <summary>
        /// Get List of GetJobsById.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPost(GetById)]
        //[ProducesResponseType(typeof(MethodResult<JobsResponseViewModel>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[SQLInjectionCheckOperation]
        ////[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetJobsByIdAsync(JobsByIdViewModel request)
        //{
        //    var methodResult = new MethodResult<UserResponseByIdModel>();
        //    var userFilterParam = _mapper.Map<JobsByIdParam>(request);
        //    var query = await _JobsServices.GetInfoUserByIdAsync(userFilterParam).ConfigureAwait(false);
        //    methodResult.Result = _mapper.Map<UserResponseByIdModel>(query);
        //    return Ok(methodResult);
        //}

        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateJobsCommand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateJobsAsync(CreateJobsCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Delete Jobs - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(MethodResult<DeleteJobsCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteJobsAsync(DeleteJobsCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Update Jobs- (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<UpdateJobsCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateJobsAsync(UpdateJobsCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
