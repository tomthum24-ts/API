using API.APPLICATION.Queries.WareHouse;
using API.APPLICATION.ViewModels.WareHouse;
using API.APPLICATION;
using API.DOMAIN;
using AutoMapper;
using BaseCommon.Attributes;
using BaseCommon.Common.MethodResult;
using BaseCommon.Common.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WareHouseController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly IWareHouseServices _WareHouseServices;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public WareHouseController(IMediator mediator, IMapper mapper, IWareHouseServices WareHouseServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _WareHouseServices = WareHouseServices;
        }

        /// <summary>
        /// GetList GroupPermission - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetList)]
        [SQLInjectionCheckOperation]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<ActionResult> GetDanhSachAsync(WareHouseRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<WareHouseResponseViewModel>>();

            DanhMucFilterParam danhMucFilterParam = new DanhMucFilterParam();
            danhMucFilterParam = _mapper.Map<DanhMucFilterParam>(request);
            danhMucFilterParam.TableName = TableConstants.WAREHOUSE_TABLENAME;

            var queryResult = await _WareHouseServices.GetDanhMucByListIdAsync(danhMucFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<WareHouseResponseViewModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<WareHouseResponseViewModel>>(queryResult.Items)
            };
            return Ok(methodResult);
        }
    }
}
