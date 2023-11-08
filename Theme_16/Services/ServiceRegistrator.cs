using System;
using Microsoft.Extensions.DependencyInjection;
using Theme_16.Services.Interfaces;
using Theme_16.Stores;
using Theme_16.ViewModels;

namespace Theme_16.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<NavigationService<NavigationStore, MainViewModel>>()
            .AddTransient<Func<MainViewModel>>(s => () => App.Services.GetRequiredService<MainViewModel>())
            .AddTransient<IDataCreator, DataCreator>()
            .AddSingleton<TransferCustomerService>()
            .AddSingleton<TransferOrderService>()
            ;
    }
}
