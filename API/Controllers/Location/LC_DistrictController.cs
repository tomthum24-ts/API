using API.APPLICATION.Commands.Location.District;
using BaseCommon.Common.MethodResult;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers.Location
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LC_DistrictController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly IMediator _mediator;

        public LC_DistrictController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// GetListDistrict - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route(GetList)]

        //public async Task<ActionResult> GetDanhSachUserAsync(DistrictRequestViewModel request)
        //{
        //    var methodResult = new MethodResult<PagingItems<DistrictResponseViewModel>>();

        //    return Ok(methodResult);
        //}

        //[HttpPost]
        //[Route(GetById)]
        //public async Task<ActionResult> GetDistrictByIdAsync(GetDistrictByIdParam param, CancellationToken cancellationToken)
        //{
        //    var query = await _iDistrictQueries.GetInfoDistrictByID(param.id, cancellationToken).ConfigureAwait(false);
        //    //methodResult.Result = _mapper.Map<DistrictViewModel>(query);
        //    return Ok(query);
        //}


        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateDistrictCommand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> CreateDistrictAsync(CreateDistrictCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
        /// <summary>
        /// Delete District - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(MethodResult<DeleteDistrictCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteDistrictAsync(DeleteDistrictCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
        /// <summary>
        /// Update District- (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<UpdateDistrictCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> UpdateDistrictAsync(UpdateDistrictCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }


    }

}
