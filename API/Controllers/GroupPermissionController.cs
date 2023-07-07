using API.APPLICATION.ViewModels.Project;
using AutoMapper;
using BaseCommon.Attributes;
using BaseCommon.Common.MethodResult;
using BaseCommon.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class GroupPermissionController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public GroupPermissionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        /// <summary>
        /// GetListProject - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetList)]
        [SQLInjectionCheckOperation]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<ActionResult> GetDanhSachAsync(ProjectRequestViewModel request)
        {
           //var methodResult = new MethodResult<PagingItems<ProjectResponseViewModel>>();

            //DanhMucFilterParam danhMucFilterParam = new DanhMucFilterParam();
            //danhMucFilterParam = _mapper.Map<DanhMucFilterParam>(request);
            //danhMucFilterParam.TableName = TableConstants.PRỌJECT_TABLENAME;

            //var queryResult = await _projectServices.GetDanhMucByListIdAsync(danhMucFilterParam).ConfigureAwait(false);
            //methodResult.Result = new PagingItems<ProjectResponseViewModel>
            //{
            //    PagingInfo = queryResult.PagingInfo,
            //    Items = _mapper.Map<IEnumerable<ProjectResponseViewModel>>(queryResult.Items)
            //};

            //return Ok(methodResult);
            return Ok();
        }

    }
}
