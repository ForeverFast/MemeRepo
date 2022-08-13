using Business.Core;
using Domain.Data;
using FluentMigrator.Runner;
using Fluxor;
using MediatR;
using MediatR.Courier.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Windows;
using Web.Core;

namespace Hybrid.WindowsApp
{
    public partial class App : Application
    {
        private IHost? _host;

        protected override void OnStartup(StartupEventArgs e)
        {
            _host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureServices(services =>
                {
                    var assemblies = new Assembly[]
                    {
                        typeof(BusinessCoreEntryPoint).Assembly,
                        typeof(HybridWindowsAppEntryPoint).Assembly,
                        typeof(WebCoreEntryPoint).Assembly,
                    };

                    var connectionString = @"Data Source=MR_DB.db;";

                    services.AddMediatR(assemblies);
                    services.AddCourier(assemblies);
                    services.AddAutoMapper(assemblies);
                    services.AddFluxor(options =>
                    {
                        options.ScanAssemblies(typeof(HybridWindowsAppEntryPoint).Assembly, typeof(WebCoreEntryPoint).Assembly);
                        options.UseReduxDevTools();
                    });

                    services.AddDAL(connectionString);
                    services.AddBLL();

                    services.AddDesktopUI();
                    services.AddWpfBlazorWebView();
                    services.AddWebUI();
#if DEBUG
                    services.AddBlazorWebViewDeveloperTools();
#endif
                })
                .Build();

            using (var scope = _host.Services.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }

            _host.Start();

            _host.Services.GetRequiredService<MainWindow>().Show();
        }
    }
}
