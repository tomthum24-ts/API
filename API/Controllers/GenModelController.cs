using API.APPLICATION.Parameters.GenDTO;
using API.APPLICATION.Queries.GenDTO;
using API.HRM.DOMAIN.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GenModelController : ControllerBase
    {
        private const string genDTO = nameof(genDTO);
        private readonly IGenDTORepoQueries _genDTORepoQueries;

        public GenModelController(IGenDTORepoQueries genDTORepoQueries)
        {
            _genDTORepoQueries = genDTORepoQueries;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(genDTO)]
        public IActionResult GenDTO(string param)
        {        
            var connect = _genDTORepoQueries.ChuoiKetNoi();
            ServerConnection sp = new ServerConnection();
            sp.ServerName = connect.ServerName;
            sp.DBName = connect.DBName;
            sp.Login = connect.Login;
            sp.Password = connect.Password;
            sp.SqlCmd = param;
            var item = _genDTORepoQueries.GetResultClass(sp);
            return Ok(item);
        }
    }
}
