using ApiCouponProduct.Database;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
var config = new ConfigurationManager();

config.AddJsonFile("appsettings.json");
serviceCollection.AddDatabase(config);

var serviceProvider = serviceCollection.BuildServiceProvider();
var _dbContext = serviceProvider.GetService<ApiCouponProductContext>();

var migrator = _dbContext.GetInfrastructure().GetService<IMigrator>();
migrator.Migrate();

Console.WriteLine("The Migration was successfull");

return 0;