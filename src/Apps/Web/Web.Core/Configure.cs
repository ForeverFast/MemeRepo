using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Web.Core.Services;

namespace Web.Core
{
    public static class Configure
    {
        public static IServiceCollection AddWebUI(this IServiceCollection services)
        {
            services.AddMudServices();

            services.AddBlazorContextMenu(options =>
            {
                options.ConfigureTemplate(defaultTemplate =>
                {
                    defaultTemplate.MenuCssClass = "memerepo-default-context-menu";
                    //defaultTemplate.MenuItemCssClass = "my-default-menu-item";
                });
            });

            services.AddSingleton<ThemeService>();

            return services;
        }
    }
}
