
using Clients.Common.Helpers;
using Clients.Data.Migrator.Enums;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Clients.Data.Migrator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Migrator starting...");
            Console.WriteLine($"Configure services...");
            var serviceProvider = CreateServices();
            Console.WriteLine($"Configure success.");

            using (var scope = serviceProvider.CreateScope())
            {
                try
                {
                    Console.WriteLine($"Choose command: ");
                    foreach (MigratorCommandType suit in (MigratorCommandType[])Enum.GetValues(typeof(MigratorCommandType)))
                    {
                        Console.WriteLine($"{(int)suit} - update");
                    }

                    switch (InitCommand())
                    {
                        case MigratorCommandType.Exit:
                            return;
                        case MigratorCommandType.Update:
                            UpdateDatabase(scope.ServiceProvider);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }

        private static MigratorCommandType InitCommand()
        {
            string _val = string.Empty;
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace)
                {
                    int val = 0;
                    bool _x = int.TryParse(key.KeyChar.ToString(), out val);
                    if (_x)
                    {
                        _val += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && _val.Length > 0)
                    {
                        _val = _val.Substring(0, (_val.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);

            return (MigratorCommandType)Convert.ToInt32(_val);
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddSQLite()
                    // Set the connection string
                    .WithGlobalConnectionString(@$"Data Source={UserFileSystemHelper.PathToUserLocalDb};")
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(ClientMigratorEntryPoint).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}
