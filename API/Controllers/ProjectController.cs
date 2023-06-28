using API.APPLICATION;
using API.APPLICATION.Commands.Project;
using API.APPLICATION.ViewModels.Project;
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
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly IMediator _mediator;
        private readonly IProjectServices _projectServices;
        private readonly IMapper _mapper;

        public ProjectController(IMediator mediator, IProjectServices projectServices, IMapper mapper)
        {
            _mediator = mediator;
            _projectServices = projectServices;
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
        public async Task<ActionResult> GetDanhSachProjectAsync(ProjectRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<ProjectResponseViewModel>>();

            DanhMucFilterParam danhMucFilterParam = new DanhMucFilterParam();
            danhMucFilterParam = _mapper.Map<DanhMucFilterParam>(request);
            danhMucFilterParam.TableName = TableConstants.PRỌJECT_TABLENAME;

            var queryResult = await _projectServices.GetDanhMucByListIdAsync(danhMucFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<ProjectResponseViewModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<ProjectResponseViewModel>>(queryResult.Items)
            };

            return Ok(methodResult);
        }
        /// <summary>
        /// Get List of GetProjectById.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(GetById)]
        [ProducesResponseType(typeof(MethodResult<ProjectResponseViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [SQLInjectionCheckOperation]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> GetProjectByIdAsync(ProjectByIdRequestViewModel request)
        {
            var methodResult = new MethodResult<ProjectResponseViewModel>();
            var queryResult = await _projectServices.GetDanhMucByIdAsync(request.Id, TableConstants.PRỌJECT_TABLENAME).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<ProjectResponseViewModel>(queryResult.Items.FirstOrDefault());
            return Ok(methodResult);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateProjectCommand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> CreateProjectAsync(CreateProjectCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
        /// <summary>
        /// Delete Project - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(MethodResult<DeleteProjectCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> DeleteProjectAsync(DeleteProjectCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
        /// <summary>
        /// Update Project- (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<UpdateProjectCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> UpdateProjectAsync(UpdateProjectCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
    

    }
}
