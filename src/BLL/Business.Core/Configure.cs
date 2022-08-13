using Microsoft.Extensions.DependencyInjection;

namespace Business.Core
{
    public static class Configure
    {
        public static IServiceCollection AddBLL(this IServiceCollection services)
        {
            return services;
        }
    }
}
