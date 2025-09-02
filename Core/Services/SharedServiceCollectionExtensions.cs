using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration;
using Shared.Services.ICDsDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public static class SharedServiceCollectionExtensions
    {
        public static IServiceCollection AddIcdDirectory(this IServiceCollection services)
        {
            services.AddSingleton<IICDDirectory, ICDDirectory>();
            return services;
        }

    }
}
