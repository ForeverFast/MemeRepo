using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Web.Core.Interfaces.Services.ViewServices;
using Web.Core.Services;
using Web.Core.Services.ViewServices;

namespace Web.Core
{
    public static class Configure
    {
        public static IServiceCollection AddWebUI(this IServiceCollection services)
        {
            services.AddMudServices(optionts =>
            {
                optionts.SnackbarConfiguration = new SnackbarConfiguration
                {
                    PositionClass = Defaults.Classes.Position.BottomRight,
                };
            });

            services.AddBlazorContextMenu(options =>
            {
                options.ConfigureTemplate(defaultTemplate =>
                {
                    defaultTemplate.MenuCssClass = "memerepo-default-context-menu";
                    //defaultTemplate.MenuItemCssClass = "my-default-menu-item";
                });
            });

            services.AddSingleton<AppInitializerService>();
            services.AddSingleton<ThemeService>();
            services.AddScoped<IDataViewService, DataViewService>();

            return services;
        }
    }
}
