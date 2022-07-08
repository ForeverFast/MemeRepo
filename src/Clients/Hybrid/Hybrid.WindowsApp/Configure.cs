using Microsoft.Extensions.DependencyInjection;

namespace Hybrid.WindowsApp
{
    internal static class Configure
    {
        public static IServiceCollection AddDesktopUI(this IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();

            return services;
        }
    }
}
