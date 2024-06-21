using OnlineShopWeb.Database;
using OnlineShopWeb.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using OnlineShopWeb;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
//using Migrater;

var serviceCollection = new ServiceCollection();
var config = new ConfigurationManager();

config.AddJsonFile("appsettings.json");

//serviceCollection.AddKeyedSingleton(serviceCollection.AddDatabase(config), "test");
//var _dbContext = serviceProvider.GetKeyedService("test");

serviceCollection.AddDatabase(config);

var serviceProvider = serviceCollection.BuildServiceProvider();

var _dbContext = serviceProvider.GetService<OnlineShopWebDbContext>();

var migrator = _dbContext.GetInfrastructure().GetService<IMigrator>();
var migrationsAssembly = _dbContext.GetInfrastructure().GetService<IMigrationsAssembly>();

//migrationsAssembly

migrator.Migrate();

////////////////////////////////

// See https://aka.ms/new-console-template for more information
Console.WriteLine("The Migration was successfull");
