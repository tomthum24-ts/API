using API.APPLICATION;
using API.APPLICATION.Commands.Login;
using API.APPLICATION.Queries.JobsCategory;
using API.APPLICATION.ViewModels.JobsCategory;
using API.APPLICATION.ViewModels.Project;
using API.APPLICATION.ViewModels.User;
using API.DOMAIN;
using API.DOMAIN.DomainObjects.User;
using AutoMapper;
using BaseCommon.Attributes;
using BaseCommon.Common.MethodResult;
using BaseCommon.Common.Response;
using BaseCommon.Model;
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
    public class JobsCategoryController : Controller
    {
        private const string GetListJobsCategory = nameof(GetListJobsCategory);
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IJobsCategoryServices _jobsCategoryServices;

        public JobsCategoryController(IMediator mediator, IMapper mapper, IJobsCategoryServices jobsCategoryServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _jobsCategoryServices = jobsCategoryServices;
        }

        /// <summary>
        /// Get info user - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<LoginCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        [Route(GetListJobsCategory)]
        public async Task<ActionResult> GetDanhSachJobsAsync(JobsCategoryRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<JobsCategoryModel>>();

            DanhMucFilterParam danhMucFilterParam = new DanhMucFilterParam();
            danhMucFilterParam = _mapper.Map<DanhMucFilterParam>(request);
            danhMucFilterParam.TableName = TableConstants.JOBSCATEGORY_TABLENAME;

            var queryResult = await _jobsCategoryServices.GetDanhMucByListIdAsync(danhMucFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<JobsCategoryModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<JobsCategoryModel>>(queryResult.Items)
            };
            return Ok(methodResult);
        }
    }
}
