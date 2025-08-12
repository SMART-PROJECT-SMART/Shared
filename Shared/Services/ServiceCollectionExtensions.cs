
using Shared.Common;
using Shared.Configuration;
using Shared.Services.ICDDirectory;

namespace Shared.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddOpenApi();
            return services;
        }

        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ICDSettings>(
                config.GetSection(SharedConstants.Config.ICD_DIRECTORY)
            );
            return services;
        }

        public static IServiceCollection AddIcdDirectoryServices(this IServiceCollection services)
        {
            services.AddSingleton<IICDDirectory, ICDDirectory.ICDDirectory>();
            return services;
        }
    }
}
