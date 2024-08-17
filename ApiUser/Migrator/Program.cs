using ApiUser.Database;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var serviceCollection = new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(runner => runner
        .AddSqlServer()
        .WithGlobalConnectionString("appsettings.json") // here the real one from DbExtension =>
        .ScanIn(typeof(Program).Assembly).For.Migrations()
    )
    .AddLogging(log => log.AddFluentMigratorConsole())
    .BuildServiceProvider();

var runner = serviceCollection.GetRequiredService<IMigrationRunner>();

runner.MigrateUp();


var serviceCollection = new ServiceCollection();
var config = new ConfigurationManager();

config.AddJsonFile("appsettings.json");
serviceCollection.AddDatabase(config);

var serviceProvider = serviceCollection.BuildServiceProvider();
var _dbContext = serviceProvider.GetService<ApiUserContext>();

var migrator = _dbContext.GetInfrastructure().GetService<IMigrator>();
migrator.Migrate();

Console.WriteLine("The Migration was successfull");

return 0;