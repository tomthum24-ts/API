using API.APPLICATION.Parameters.Menu;
using API.APPLICATION.Queries.Menu;
using API.APPLICATION.ViewModels;
using API.DOMAIN.DTOs.Menu;
using AutoMapper;
using BaseCommon.Attributes;
using BaseCommon.Common.MethodResult;
using BaseCommon.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private readonly IMapper _mapper;
        private readonly IMenuServices _menuServices;

        public MenuController(IMapper mapper, IMenuServices menuServices)
        {
            _mapper = mapper;
            _menuServices = menuServices;
        }

        /// <summary>
        /// GetListMenu - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetList)]
        [SQLInjectionCheckOperation()]
        [AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        //[AllowAnonymous]
        public async Task<ActionResult> GetListMenuAsync()
        {
            var methodResult = new MethodResult<IEnumerable<MenuDTO>>();
            //var param = _mapper.Map<MenuFilterParam>(request);
            var queryResult = await _menuServices.GetListMenuAsync().ConfigureAwait(false);
            methodResult.Result = queryResult;
            return Ok(methodResult);
        }
    }
}
