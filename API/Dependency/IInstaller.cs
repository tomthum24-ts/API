using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace API.Dependency
{
    public interface IInstaller
    {
        void InstallServices( IServiceCollection services,IConfiguration configuration);
    }
}
