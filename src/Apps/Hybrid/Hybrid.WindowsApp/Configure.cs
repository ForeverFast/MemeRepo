using Domain.Core.Interfaces;
using Hybrid.WindowsApp.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Hybrid.WindowsApp
{
    internal static class Configure
    {
        public static IServiceCollection AddDesktopUI(this IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<IFileStorageProvider, WindowsFileStorageProvider>();
            services.AddSingleton<IFileStorageDialogProvider, WindowsDialogProvider>();
            
            return services;
        }
    }
}
