using BaseCommon.Common.MethodResult;
using Microsoft.Extensions.DependencyInjection;

namespace BaseCommon.Common.AttachFile
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMedia(this IServiceCollection services, string sectionName = Settings.MediaConfig)
        {
            var mediaConfig = AppSettings.Instance.Get<MediaOptions>(sectionName);

            services.Configure<MediaOptions>(_ =>
            {
                _.MediaUploadUrl = mediaConfig.MediaUploadUrl;

                _.MediaUrl = mediaConfig.MediaUrl;

                _.FolderForWeb = mediaConfig.FolderForWeb;

                _.PermittedExtensions = mediaConfig.PermittedExtensions;

                _.SizeLimit = mediaConfig.SizeLimit;
            });

            return services;
        }
    }
}