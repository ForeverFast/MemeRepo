using DALQueryChain.Linq2Db;
using Domain.Data.Context;
using FluentMigrator.Runner;
using LinqToDB.AspNet;
using LinqToDB.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Domain.Data
{
    public static class Configure
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, string connectionString)
        {
            services.AddLinqToDBContext<MemeRepoClientContext>((provider, options) =>
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

            services.AddQueryChain(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
