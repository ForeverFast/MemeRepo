using DALQueryChain.Linq2Db;
using FluentMigrator.Runner;
using LinqToDB.AspNet;
using LinqToDB.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Data
{
    public static class Configure
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, string connectionString)
        {
            services.AddLinqToDBContext<MemeRepoDbContext>((provider, options) =>
            {
                options.UseSQLite(connectionString);
            });

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(DomainDataEntryPoint).Assembly).For.Migrations().For.EmbeddedResources())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider();

            services.AddQueryChain(typeof(DomainDataEntryPoint).Assembly);

            return services;
        }
    }
}
