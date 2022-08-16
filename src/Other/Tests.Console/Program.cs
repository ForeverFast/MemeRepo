
//using Domain.Data;
//using FluentMigrator.Runner;
//using Microsoft.Extensions.DependencyInjection;

//IServiceCollection services = new ServiceCollection();
//IServiceProvider serviceProvider = services.AddDAL(@"Data Source=M:\Programming\CSharp\MemeRepo\src\DAL\Domain.Data\MR_DB.db;").BuildServiceProvider();

//using (var scope = serviceProvider.CreateScope())
//{
//    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
//    runner.MigrateUp();
//}

var tt = Test.val1;

if (tt == (Test.val1 | Test.val2))
{
    Console.WriteLine("govno");
}








enum Test
{
    val1,
    val2,
    val3,
}