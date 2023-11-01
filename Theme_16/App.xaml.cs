using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using Theme_16.Services;
using Theme_16.Stores;
using Theme_16.ViewModels;
using Theme_16.Views;

namespace Theme_16
{

    public partial class App : Application
    {
        private static IHost _host;

        public static IHost Host => _host??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddServices()
            .AddViewModels()       
            .AddViews()
            .AddStores()
            ;

        public static IServiceProvider Services => Host.Services;

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            base.OnStartup(e);
            await host.StartAsync();

           // App.Services.GetRequiredService<NavigationStore>().CurrentViewModel = new LoginViewModel();

            MainWindow? mainWindow = App.Services.GetService<MainWindow>();
            if (mainWindow != null)
                mainWindow.Show();

        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            using (Host) await Host.StopAsync();
        }
    }
}
