using API.APPLICATION;
using API.APPLICATION.Queries;
using API.APPLICATION.Queries.GenDTO;
using API.APPLICATION.Queries.Location;
using API.APPLICATION.Queries.Media;
using API.APPLICATION.Queries.Menu;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface;
using API.INFRASTRUCTURE.Interface.Location;
using API.INFRASTRUCTURE.Interface.Media;
using API.INFRASTRUCTURE.Interface.RefreshToken;
using API.INFRASTRUCTURE.Repositories;
using API.INFRASTRUCTURE.Repositories.FileAttach;
using API.INFRASTRUCTURE.Repositories.Permission;
using API.INFRASTRUCTURE.Repositories.User;
using BaseCommon.Authorization;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.Report.Infrastructures;
using BaseCommon.Common.Report.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Dependency
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NGaF5cXmdCdkx0WmFZfV1gcF9DaFZUQ2YuP1ZhSXxQdkJhX39adXxQQWRVWEc=");
            services.AddSingleton<IGenDTORepoQueries, GenDTORepoQueries>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IUserSessionInfo, UserSessionInfo>();
            services.AddScoped<IPermissionChecker, PermissionChecker>();
            //services.AddScoped<IDistributedCached<T>, DistributedCache<T>>();
            //User
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserServices, UserServices>();

            //Project
            services.AddScoped<IProjectServices, ProjectServices>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            //Location
            services.AddScoped<ILocationServices, LocationServices>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IProvinceServices, ProvinceServices>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IDistrictServices, DistrictServices>();
            services.AddScoped<IVillageRepository, VillageRepository>();
            services.AddScoped<IVillageServices, VillageServices>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            //Menu
            services.AddScoped<IMenuServices, MenuServices>();
            //Credetial
            services.AddScoped<ICredentialRepository, CredentialRepository>();
            services.AddScoped<IJWTManagerRepository, JWTManagerRepository>();
            //FileAttach
            services.AddScoped<IAttachmentFileRepository, AttachmentFileRepository>();
            //Report
            services.AddScoped<IReportQueries, ReportQueries>();
            services.AddScoped<IExportService, ExportService>();
            services.AddScoped<ISYSBieuMauQueries, SYSBieuMauQueries>();


        }
    }
}