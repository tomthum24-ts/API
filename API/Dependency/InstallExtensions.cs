using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace API.Dependency
{
    public static class InstallExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection service, IConfiguration configuration)
        {
            var installer = typeof(Startup).Assembly.ExportedTypes.Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
            installer.ForEach(installer => installer.InstallServices(service, configuration));
        }
    }
}