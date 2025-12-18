using API.APPLICATION.Commands.JobApplys;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class JobApplysController :  ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        //private readonly IJobApplysServices _JobApplysServices;

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public JobApplysController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        //[HttpPost]
        //[Route(GetList)]
        //[SQLInjectionCheckOperation]
        ////[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        //[AllowAnonymous]
        //public async Task<ActionResult> GetDanhSachJobApplysAsync(JobApplysRequestViewModel request)
        //{
        //    var methodResult = new MethodResult<PagingItems<JobApplysResponseViewModel>>();
        //    var userFilterParam = _mapper.Map<JobApplysFilterParam>(request);

        //    var queryResult = await _JobApplysServices.GetJobApplysPagingAsync(userFilterParam).ConfigureAwait(false);
        //    methodResult.Result = new PagingItems<JobApplysResponseViewModel>
        //    {
        //        PagingInfo = queryResult.PagingInfo,
        //        Items = _mapper.Map<IEnumerable<JobApplysResponseViewModel>>(queryResult.Items)
        //    };

        //    return Ok(methodResult);
        //}
        /// <summary>
        /// Get List of GetJobApplysById.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPost(GetById)]
        //[ProducesResponseType(typeof(MethodResult<JobApplysResponseViewModel>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[SQLInjectionCheckOperation]
        ////[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetJobApplysByIdAsync(JobApplysByIdViewModel request)
        //{
        //    var methodResult = new MethodResult<UserResponseByIdModel>();
        //    var userFilterParam = _mapper.Map<JobApplysByIdParam>(request);
        //    var query = await _JobApplysServices.GetInfoUserByIdAsync(userFilterParam).ConfigureAwait(false);
        //    methodResult.Result = _mapper.Map<UserResponseByIdModel>(query);
        //    return Ok(methodResult);
        //}

        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateJobApplysCommand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateJobApplysAsync(CreateJobApplysCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Delete JobApplys - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(MethodResult<DeleteJobApplysCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteJobApplysAsync(DeleteJobApplysCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Update JobApplys- (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<UpdateJobApplysCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateJobApplysAsync(UpdateJobApplysCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
