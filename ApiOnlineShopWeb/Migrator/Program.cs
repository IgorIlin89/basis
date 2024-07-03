using ApiOnlineShopWeb.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics;
using System.Runtime.InteropServices;

////////////////// OPTION 1 using Migrater;
var serviceCollection = new ServiceCollection();
var config = new ConfigurationManager();

config.AddJsonFile("appsettings.json");
serviceCollection.AddDatabase(config);

var serviceProvider = serviceCollection.BuildServiceProvider();
var _dbContext = serviceProvider.GetService<ApiOnlineShopWebContext>();

//Console.WriteLine(_dbContext.Model.AnnotationsToDebugString());

var migrator = _dbContext.GetInfrastructure().GetService<IMigrator>();
migrator.Migrate();

Console.WriteLine("The Migration was successfull");

return 0; //remove and comment option 1 to run option 2

////////////////////////////////

// OPTION 2 using .net CLI // OPTION 2 using .net CLI // OPTION 2 using .net CLI

var changeToExePath = $"cd {AppContext.BaseDirectory}";
var changeToWorkPath =
    $"cd ..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}" +
    $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}OnlineShopWeb";

var executeMigration = "dotnet ef database update --context OnlineShopWebDbContext";

using (Process myProcess = new Process())
{
    myProcess.StartInfo.UseShellExecute = false;
    //myProcess.StartInfo.WorkingDirectory = "";

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        myProcess.StartInfo.FileName = "cmd.exe";
    }
    else
    {
        myProcess.StartInfo.FileName = "/bin/bash";

    }

    myProcess.StartInfo.RedirectStandardInput = true;
    //myProcess.StartInfo.Arguments = $"\"dotnet ef database update --context OnlineShopWebDbContext\"";
    myProcess.StartInfo.CreateNoWindow = false;
    myProcess.StartInfo.RedirectStandardOutput = true;
    myProcess.Start();

    var streamWriter = myProcess.StandardInput;

    if (streamWriter.BaseStream.CanWrite)
    {
        streamWriter.WriteLine("dotnet tool install --global dotnet-ef");
        streamWriter.WriteLine(changeToExePath);
        streamWriter.WriteLine(changeToWorkPath);
        streamWriter.WriteLine(executeMigration);
        streamWriter.BaseStream.Close();
    }

    string output = myProcess.StandardOutput.ReadToEnd();
    myProcess.WaitForExit();
    Console.WriteLine(output);
    Console.WriteLine("The Migration was successfull");
}