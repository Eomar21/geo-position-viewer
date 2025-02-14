using GeoPositionViewer.App.ViewModels;
using GeoPositionViewer.App.Views;
using GeoPositionViewer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace GeoPositionViewer.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost m_Host;
        public IServiceProvider m_ServiceProvider => m_Host.Services;

        public App()
        {
            m_Host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                // Services
                services.WithPositioningServices();

                // ViewModels
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<PositionViewModel>();

                // Views
                services.AddTransient<MainWindow>();
                services.AddTransient<PositionView>();
            }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await m_Host.StartAsync();
            var mainWindow = m_ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            await m_Host.StopAsync();
            m_Host.Dispose();
        }
    }
}