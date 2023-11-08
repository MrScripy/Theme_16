using Microsoft.Extensions.DependencyInjection;

namespace Theme_16.Stores
{
    internal static class StoreRegistrator
    {
        public static IServiceCollection AddStores(this IServiceCollection services) => services
            .AddSingleton<NavigationStore>()
            ;
    }
}
