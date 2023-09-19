using Microsoft.Extensions.DependencyInjection;

namespace Theme_16.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services;
            //.AddSingleton<IUserData, UserDataCreator>()
            
    }
}
