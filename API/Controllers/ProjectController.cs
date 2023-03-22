using API.APPLICATION.Commands.Project;
using API.APPLICATION.ViewModels.Project;
using BaseCommon.Common.MethodResult;
using BaseCommon.Common.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// GetListProject - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetList)]

        public async Task<ActionResult> GetDanhSachUserAsync(ProjectRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<ProjectResponseViewModel>>();

            return Ok(methodResult);
        }

        //[HttpPost]
        //[Route(GetById)]
        //public async Task<ActionResult> GetProjectByIdAsync(GetProjectByIdParam param, CancellationToken cancellationToken)
        //{
        //    var query = await _iProjectQueries.GetInfoProjectByID(param.id, cancellationToken).ConfigureAwait(false);
        //    //methodResult.Result = _mapper.Map<ProjectViewModel>(query);
        //    return Ok(query);
        //}


        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateProjectCommand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]

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
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        public async Task<IActionResult> UpdateProjectAsync(UpdateProjectCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
    

    }
}
