﻿using API.APPLICATION.Commands.WareHouseIn;
using API.APPLICATION.Parameters.WareHouseIn;
using API.APPLICATION.Queries.WareHouseIn;
using API.APPLICATION.ViewModels.WareHouseIn;
using API.APPLICATION.ViewModels.WareHouseInDetail;
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
    public class WareHouseInController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);
        private readonly IWareHouseInServices _wareHouseInServices;

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public WareHouseInController(IMediator mediator, IMapper mapper, IWareHouseInServices wareHouseInServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _wareHouseInServices = wareHouseInServices;
        }

        /// <summary>
        /// GetListWareHouseIn - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetList)]
        [SQLInjectionCheckOperation]
        public async Task<ActionResult> GetDanhSachWareHouseInAsync(WareHouseInRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<WareHouseInResponseViewModel>>();
            var userFilterParam = _mapper.Map<WareHouseInFilterParam>(request);

            var queryResult = await _wareHouseInServices.GetWareHouseInPagingAsync(userFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<WareHouseInResponseViewModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<WareHouseInResponseViewModel>>(queryResult.Items)
            };

            return Ok(methodResult);
        }

        /// <summary>
        /// Get List of GetWareHouseInById.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(GetById)]
        [ProducesResponseType(typeof(MethodResult<WareHouseInDetailViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [SQLInjectionCheckOperation]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        //[AllowAnonymous]
        public async Task<IActionResult> GetWareHouseInByIdAsync(WareHouseInByIdViewModel request)
        {
            var methodResult = new MethodResult<WareHouseInDetailViewModel>();
            var userFilterParam = _mapper.Map<WareHouseInByIdParam>(request);
            methodResult.Result = await _wareHouseInServices.GetWareHouseInByIdAsync(userFilterParam).ConfigureAwait(false);
            return Ok(methodResult);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateWareHouseInCommand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        //[AllowAnonymous]
        public async Task<IActionResult> CreateWareHouseInAsync(CreateWareHouseInCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Delete WareHouseIn - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(MethodResult<DeleteWareHouseInCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        //[AllowAnonymous]
        public async Task<IActionResult> DeleteWareHouseInAsync(DeleteWareHouseInCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Update WareHouseIn- (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<UpdateWareHouseInCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        //[AllowAnonymous]
        public async Task<IActionResult> UpdateWareHouseInAsync(UpdateWareHouseInCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
    }
}