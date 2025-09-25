using Core.Services.ICDsDirectory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
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
