using Microsoft.Extensions.DependencyInjection;
using Theme_16.Services.Interfaces;

namespace Theme_16.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddSingleton<IDataCreator, DataCreator>()
            ;
    }
}
