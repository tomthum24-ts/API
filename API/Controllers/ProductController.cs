using API.APPLICATION.Queries.Vehicle;
using API.APPLICATION.ViewModels.Unit;
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
using API.APPLICATION.Queries.Product;
using API.APPLICATION.ViewModels.Product;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly IProductServices _prodcutServices;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator, IMapper mapper, IProductServices prodcutServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _prodcutServices = prodcutServices;
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
        public async Task<ActionResult> GetDanhSachAsync(ProductRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<ProductResponseViewModel>>();

            DanhMucFilterParam danhMucFilterParam = new DanhMucFilterParam();
            danhMucFilterParam = _mapper.Map<DanhMucFilterParam>(request);
            danhMucFilterParam.TableName = TableConstants.PRODUCT_TABLENAME;

            var queryResult = await _prodcutServices.GetDanhMucByListIdAsync(danhMucFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<ProductResponseViewModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<ProductResponseViewModel>>(queryResult.Items)
            };
            return Ok(methodResult);
        }
    }
}
