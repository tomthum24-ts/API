using API.APPLICATION.Commands.Location.Village;
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
    public class LC_VillageController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly IMediator _mediator;

        public LC_VillageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// GetListVillage - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route(GetList)]

        //public async Task<ActionResult> GetDanhSachUserAsync(VillageRequestViewModel request)
        //{
        //    var methodResult = new MethodResult<PagingItems<VillageResponseViewModel>>();

        //    return Ok(methodResult);
        //}

        //[HttpPost]
        //[Route(GetById)]
        //public async Task<ActionResult> GetVillageByIdAsync(GetVillageByIdParam param, CancellationToken cancellationToken)
        //{
        //    var query = await _iVillageQueries.GetInfoVillageByID(param.id, cancellationToken).ConfigureAwait(false);
        //    //methodResult.Result = _mapper.Map<VillageViewModel>(query);
        //    return Ok(query);
        //}


        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateVillageCommand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> CreateVillageAsync(CreateVillageCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
        /// <summary>
        /// Delete Village - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(MethodResult<DeleteVillageCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteVillageAsync(DeleteVillageCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
        /// <summary>
        /// Update Village- (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<UpdateVillageCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> UpdateVillageAsync(UpdateVillageCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }


    }
}
