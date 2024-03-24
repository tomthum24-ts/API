using API.APPLICATION.Commands.WareHouseOut;
using API.APPLICATION.Parameters.WareHouseIn;
using API.APPLICATION.Parameters.WareHouseOut;
using API.APPLICATION.Queries.WareHouseOut;
using API.APPLICATION.ViewModels.WareHouseIn;
using API.APPLICATION.ViewModels.WareHouseInDetail;
using API.APPLICATION.ViewModels.WareHouseOut;
using API.APPLICATION.ViewModels.WareHouseOutDetail;
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
    public class WareHouseOutController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);
        private readonly IWareHouseOutServices _WareHouseOutServices;

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public WareHouseOutController(IMediator mediator, IMapper mapper, IWareHouseOutServices WareHouseOutServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _WareHouseOutServices = WareHouseOutServices;
        }

        /// <summary>
        /// GetListWareHouseOut - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetList)]
        [SQLInjectionCheckOperation]
        public async Task<ActionResult> GetDanhSachWareHouseOutAsync(WareHouseOutRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<WareHouseOutResponseViewModel>>();
            var userFilterParam = _mapper.Map<WareHouseOutFilterParam>(request);

            var queryResult = await _WareHouseOutServices.GetWareHouseOutPagingAsync(userFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<WareHouseOutResponseViewModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<WareHouseOutResponseViewModel>>(queryResult.Items)
            };

            return Ok(methodResult);
        }

        /// <summary>
        /// Get List of GetWareHouseOutById.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(GetById)]
        [ProducesResponseType(typeof(MethodResult<WareHouseOutDetailResponseViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [SQLInjectionCheckOperation]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        //[AllowAnonymous]
        public async Task<IActionResult> GetWareHouseOutByIdAsync(WareHouseOutByIdViewModel request)
        {
            var methodResult = new MethodResult<WareHouseOutDetailViewModel>();
            var userFilterParam = _mapper.Map<WareHouseOutByIdParam>(request);
            methodResult.Result = await _WareHouseOutServices.GetWareHouseOutByIdAsync(userFilterParam).ConfigureAwait(false);
            return Ok(methodResult);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateWareHouseOutCommand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        //[AllowAnonymous]
        public async Task<IActionResult> CreateWareHouseOutAsync(CreateWareHouseOutCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Delete WareHouseOut - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(MethodResult<DeleteWareHouseOutCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        //[AllowAnonymous]
        public async Task<IActionResult> DeleteWareHouseOutAsync(DeleteWareHouseOutCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Update WareHouseOut- (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<UpdateWareHouseOutCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        //[AllowAnonymous]
        public async Task<IActionResult> UpdateWareHouseOutAsync(UpdateWareHouseOutCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
    }
}