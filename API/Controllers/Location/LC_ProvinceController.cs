using API.APPLICATION.Commands.Location.Province;
using BaseCommon.Common.MethodResult;
using BaseCommon.Common.Response;
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
    public class LC_ProvinceController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly IMediator _mediator;

        public LC_ProvinceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// GetListProvince - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route(GetList)]

        //public async Task<ActionResult> GetDanhSachUserAsync(ProvinceRequestViewModel request)
        //{
        //    var methodResult = new MethodResult<PagingItems<ProvinceResponseViewModel>>();

        //    return Ok(methodResult);
        //}

        //[HttpPost]
        //[Route(GetById)]
        //public async Task<ActionResult> GetProvinceByIdAsync(GetProvinceByIdParam param, CancellationToken cancellationToken)
        //{
        //    var query = await _iProvinceQueries.GetInfoProvinceByID(param.id, cancellationToken).ConfigureAwait(false);
        //    //methodResult.Result = _mapper.Map<ProvinceViewModel>(query);
        //    return Ok(query);
        //}


        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateProvinceCommand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> CreateProvinceAsync(CreateProvinceCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
        /// <summary>
        /// Delete Province - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(MethodResult<DeleteProvinceCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteProvinceAsync(DeleteProvinceCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
        /// <summary>
        /// Update Province- (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<UpdateProvinceCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> UpdateProvinceAsync(UpdateProvinceCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }


    }
}
