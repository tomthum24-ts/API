using API.APPLICATION.Commands.Location.Province;
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
    public class LC_ProvinceController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IProvinceServices _provinceServices;
        public LC_ProvinceController(IMediator mediator, IMapper mapper, IProvinceServices provinceServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _provinceServices = provinceServices;
        }

        /// <summary>
        /// GetListProvince - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetList)]

        public async Task<ActionResult> GetDanhSachProvinceAsync(ProvinceRequestViewModel request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<PagingItems<ProvinceDTO>>();
            var param = _mapper.Map<ProvinceFilterParam>(request);
            var queryResult = await _provinceServices.GetAllProvincePaging(param).ConfigureAwait(false);

            methodResult.Result = new PagingItems<ProvinceDTO>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<ProvinceDTO>>(queryResult.Items)
            };
            return Ok(methodResult);
        }
        /// <summary>
        /// GetProvinceById - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetById)]
        [ProducesResponseType(typeof(MethodResult<ResponseByIdViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetProvinceByIdAsync(RequestByIdViewModel param, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<Dictionary<string, string>>();
            var query = await _provinceServices.GetInfoByIdAsync(param).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<UserViewModel>(query);
            Dictionary<string, string> data = query.ToDictionary(x => x.ObjKey, x => StringHelpers.Normalization(x.ObjValue));
            methodResult.Result = data;
            return Ok(methodResult);
        }


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
