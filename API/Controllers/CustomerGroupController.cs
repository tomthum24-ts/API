using API.APPLICATION;
using API.APPLICATION.Queries.CustomerGroup;
using API.APPLICATION.Queries.Vehicle;
using API.APPLICATION.ViewModels.CustomerGroup;
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
    public class CustomerGroupController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly ICustomerGroupServices _customerGroupServices;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CustomerGroupController(IMediator mediator, IMapper mapper, ICustomerGroupServices customerGroupServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _customerGroupServices = customerGroupServices;
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
        public async Task<ActionResult> GetDanhSachAsync(CustomerGroupRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<CustomerGroupResponseViewModel>>();

            DanhMucFilterParam danhMucFilterParam = new DanhMucFilterParam();
            danhMucFilterParam = _mapper.Map<DanhMucFilterParam>(request);
            danhMucFilterParam.TableName = TableConstants.GROUP_TABLENAME;

            var queryResult = await _customerGroupServices.GetDanhMucByListIdAsync(danhMucFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<CustomerGroupResponseViewModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<CustomerGroupResponseViewModel>>(queryResult.Items)
            };
            return Ok(methodResult);
        }
    }
}
