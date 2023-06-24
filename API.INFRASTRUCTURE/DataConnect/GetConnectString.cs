
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace API.INFRASTRUCTURE.DataConnect
{
    public class GetConnectString
    {
        private readonly IConfiguration _configuration;
        protected readonly IWebHostEnvironment _env;

        public GetConnectString(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        public string GetConnect()
        {

            if (_env.EnvironmentName == "Development")
            {
                return _configuration.GetConnectionString("Develop");
            }
            return _configuration.GetConnectionString("Product");
        }

    }
}
