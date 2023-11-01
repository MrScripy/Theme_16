using Microsoft.Extensions.DependencyInjection;

namespace Theme_16.Views
{
    internal static class ViewsRegistrator
    {
        public static IServiceCollection AddViews(this IServiceCollection services) => services
            .AddSingleton<MainWindow>()
            ;
    }
}
