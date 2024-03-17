using API.APPLICATION;
using API.APPLICATION.Queries.Unit;
using API.APPLICATION.Queries.Vehicle;
using API.APPLICATION.ViewModels.Unit;
using API.APPLICATION.ViewModels.Vehicle;
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
    public class UnitController : Controller
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly IUnitServices _unitServices;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UnitController(IMediator mediator, IMapper mapper, IUnitServices unitServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _unitServices = unitServices;
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
        public async Task<ActionResult> GetDanhSachAsync(UnitRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<UnitResponseViewModel>>();

            DanhMucFilterParam danhMucFilterParam = new DanhMucFilterParam();
            danhMucFilterParam = _mapper.Map<DanhMucFilterParam>(request);
            danhMucFilterParam.TableName = TableConstants.UNIT_TABLENAME;

            var queryResult = await _unitServices.GetDanhMucByListIdAsync(danhMucFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<UnitResponseViewModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<UnitResponseViewModel>>(queryResult.Items)
            };
            return Ok(methodResult);
        }
    }
}
