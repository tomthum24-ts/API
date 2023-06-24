using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace API.INFRASTRUCTURE.DataConnect
{
    public class DapperContext
    {
        protected readonly IWebHostEnvironment _env;
        private readonly GetConnectString _getConnectString;
        public DapperContext( IWebHostEnvironment env, GetConnectString getConnectString)
        {
            _env = env;
            _getConnectString = getConnectString;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_getConnectString.GetConnect());
    }
}
