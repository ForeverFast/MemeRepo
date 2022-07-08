using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Web.Core;

namespace Hybrid.WindowsApp
{
    public partial class App : Application
    {
        private IHost _host;

        protected override void OnStartup(StartupEventArgs e)
        {
            _host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureServices(services =>
                {
                    services.AddWpfBlazorWebView();

                    services.AddDesktopUI();
                    services.AddWebUI();
                })
                .Build();

            _host.Start();

            _host.Services.GetRequiredService<MainWindow>().Show();
        }
    }
}
