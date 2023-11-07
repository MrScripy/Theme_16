using Microsoft.Extensions.DependencyInjection;
using Theme_16.ViewModels.DialogsVM;

namespace Theme_16.ViewModels
{
    internal static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddTransient<LoginViewModel>()
            .AddTransient<MainViewModel>()
            .AddTransient<AddClientDialogViewModel>()
            .AddTransient<AddOrderDialogViewModel>()
            .AddTransient<ChangeClientInfoDialogViewModel>()
            ;
    }
}
