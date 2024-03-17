using API.APPLICATION;
using API.APPLICATION.Parameters.WareHouseIn;
using API.APPLICATION.Queries;
using API.APPLICATION.Queries.Vehicle;
using API.APPLICATION.Queries.WareHouseIn;
using API.APPLICATION.ViewModels.Permission;
using API.APPLICATION.ViewModels.Vehicle;
using API.APPLICATION.ViewModels.WareHouseIn;
using API.DOMAIN;
using AutoMapper;
using BaseCommon.Attributes;
using BaseCommon.Common.MethodResult;
using BaseCommon.Common.Response;
using BaseCommon.Model;
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
    public class VehicleController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly IVehicleServices _vehicleServices;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public VehicleController(IMediator mediator, IMapper mapper, IVehicleServices vehicleServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _vehicleServices = vehicleServices;
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
        public async Task<ActionResult> GetDanhSachAsync(VehicleRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<VehicleResponseViewModel>>();

            DanhMucFilterParam danhMucFilterParam = new DanhMucFilterParam();
            danhMucFilterParam = _mapper.Map<DanhMucFilterParam>(request);
            danhMucFilterParam.TableName = TableConstants.VEHICLE_TABLENAME;

            var queryResult = await _vehicleServices.GetDanhMucByListIdAsync(danhMucFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<VehicleResponseViewModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<VehicleResponseViewModel>>(queryResult.Items)
            };
            return Ok(methodResult);
        }
    }
}
