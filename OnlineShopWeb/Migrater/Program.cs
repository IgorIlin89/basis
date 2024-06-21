using OnlineShopWeb.Database;
using OnlineShopWeb.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using OnlineShopWeb;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics;
//using Migrater;

var serviceCollection = new ServiceCollection();
var config = new ConfigurationManager();

config.AddJsonFile("appsettings.json");

//serviceCollection.AddKeyedSingleton(serviceCollection.AddDatabase(config), "test");
//var _dbContext = serviceProvider.GetKeyedService("test");

serviceCollection.AddDatabase(config);

var serviceProvider = serviceCollection.BuildServiceProvider();

//var connectionString = "Server=IGOR\\SQLEXPRESS;Database=OnlineShopWebDb;User Id=OnlineShobWebDatabase;Password=123456;Integrated Security=false;MultipleActiveResultSets=true;TrustServerCertificate=True";

//var optionsBuilder = new DbContextOptionsBuilder<OnlineShopWebDbContext>();
//optionsBuilder.UseSqlServer(connectionString);

//var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

var _dbContext = serviceProvider.GetService<OnlineShopWebDbContext>();

Console.WriteLine(_dbContext.Model.AnnotationsToDebugString());

var migrator = _dbContext.GetInfrastructure().GetService<IMigrator>();

var migrationsAssembly = _dbContext.GetInfrastructure().GetService<IMigrationsAssembly>();
//migrationsAssembly.

migrator.Migrate();

////////////////////////////////

// See https://aka.ms/new-console-template for more information
Console.WriteLine("The Migration was successfull");

//Process process = new Process();
//process.StartInfo.FileName = "dotnet";
//process.StartInfo.Arguments = "--version";
//process.StartInfo.UseShellExecute = false;
//process.StartInfo.RedirectStandardOutput = true;
//process.Start();

//string output = process.StandardOutput.ReadToEnd();
//process.WaitForExit();
//Console.WriteLine(output);
