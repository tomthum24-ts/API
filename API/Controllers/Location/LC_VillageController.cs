using API.APPLICATION.Commands.Location.Village;
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
    public class LC_VillageController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IVillageServices _villageServices;

        public LC_VillageController(IMediator mediator, IMapper mapper, IVillageServices villageServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _villageServices = villageServices;
        }


        /// <summary>
        /// GetListVillage - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetList)]

        public async Task<ActionResult> GetDanhSachVillageAsync(VillageRequestViewModel request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<PagingItems<VillageDTO>>();
            var param = _mapper.Map<VillageFilterParam>(request);
            var queryResult = await _villageServices.GetAllVillagePaging(param).ConfigureAwait(false);

            methodResult.Result = new PagingItems<VillageDTO>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<VillageDTO>>(queryResult.Items)
            };
            return Ok(methodResult);
        }
        /// <summary>
        /// GetVillageById - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetById)]
        [ProducesResponseType(typeof(MethodResult<ResponseByIdViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetVillageByIdAsync(RequestByIdViewModel param, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<Dictionary<string, string>>();
            var query = await _villageServices.GetInfoByIdAsync(param).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<UserViewModel>(query);
            Dictionary<string, string> data = query.ToDictionary(x => x.ObjKey, x => StringHelpers.Normalization(x.ObjValue));
            methodResult.Result = data;
            return Ok(methodResult);
        }

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
