using ApiOnlineShopWeb.Database;
using ApiOnlineShopWeb.ExceptionHandling;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

try
{
    var bootstrapLoggingConfiguration = new LoggerConfiguration()
        .WriteTo.File("ApiOnlineShopWeb_Fatal.log");
    Log.Logger = bootstrapLoggingConfiguration.CreateBootstrapLogger();

    // Add services to the container.
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        //options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

    builder.Services.AddDatabase(builder.Configuration);
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var loggingConfiguration = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProcessId()
        .Enrich.WithProcessName()
        .Enrich.WithMachineName();

    var logger = loggingConfiguration.CreateLogger();
    builder.Host.UseSerilog(logger);

    var app = builder.Build();

    app.UseMiddleware<MiddlewareCustomExceptionHandling>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseSerilogRequestLogging();

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Error during Start Api");
}
finally
{
    Log.CloseAndFlush();
}