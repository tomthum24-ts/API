using API.APPLICATION.Commands.Location.District;
using API.APPLICATION.Parameters.Location;
using API.APPLICATION.Queries.Location;
using API.APPLICATION.ViewModels.ByIdViewModel;
using API.APPLICATION.ViewModels.Location;
using API.DOMAIN.DTOs.Location;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Common.Response;
using BaseCommon.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
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
        private readonly IMapper _mapper;
        private readonly IDistrictServices _districtServices;
        public LC_DistrictController(IMediator mediator, IMapper mapper, IDistrictServices districtServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _districtServices = districtServices;
        }

        /// <summary>
        /// GetListDistrict - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetList)]

        public async Task<ActionResult> GetDanhSachDistrictAsync(DistrictRequestViewModel request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<PagingItems<DistrictDTO>>();
            var param = _mapper.Map<DistrictFilterParam>(request);
            var queryResult = await _districtServices.GetDistrictAsync(param).ConfigureAwait(false);

            methodResult.Result = new PagingItems<DistrictDTO>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<DistrictDTO>>(queryResult.Items)
            };
            return Ok(methodResult);
        }
        /// <summary>
        /// GetDistrictById - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetById)]
        [ProducesResponseType(typeof(MethodResult<ResponseByIdViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetDistrictByIdAsync(RequestByIdViewModel param, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<Dictionary<string, string>>();
            var query = await _districtServices.GetInfoByIdAsync(param).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<UserViewModel>(query);
            Dictionary<string, string> data = query.ToDictionary(x => x.ObjKey, x => StringHelpers.Normalization(x.ObjValue));
            methodResult.Result = data;
            return Ok(methodResult);
        }

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
