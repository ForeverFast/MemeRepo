using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Web.Core.Services.ViewServices;

namespace Web.Core
{
    public static class Configure
    {
        public static IServiceCollection AddWebUI(this IServiceCollection services)
        {
            services.AddMudServices();

            services.AddSingleton<ThemeService>();

            return services;
        }
    }
}
