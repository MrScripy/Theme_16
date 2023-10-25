using Microsoft.Extensions.DependencyInjection;

namespace Theme_16.ViewModels
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<LoginViewModel>()
            .AddSingleton<MainViewModel>()
            ;
    }
}
