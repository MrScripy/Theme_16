using Microsoft.Extensions.DependencyInjection;
using Theme_16.ViewModels.DialogsVM;

namespace Theme_16.ViewModels
{
    internal class ViewModelLocator
    {

        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();

        public LoginViewModel LoginViewModel => App.Services.GetRequiredService<LoginViewModel>();

        public MainViewModel MainViewModel => App.Services.GetRequiredService<MainViewModel>();

        public AddClientDialogViewModel AddClientDialogViewModel => App.Services.GetRequiredService<AddClientDialogViewModel>();
        public AddOrderDialogViewModel AddOrderDialogViewModel => App.Services.GetRequiredService<AddOrderDialogViewModel>();
    }
}
