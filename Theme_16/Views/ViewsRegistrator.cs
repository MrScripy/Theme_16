using Microsoft.Extensions.DependencyInjection;
using Theme_16.Views.Windows;

namespace Theme_16.Views
{
    internal static class ViewsRegistrator
    {
        public static IServiceCollection AddViews(this IServiceCollection services) => services
            .AddSingleton<LoginWindow>()
            .AddSingleton<MainWindow>()
            ;
    }
}
