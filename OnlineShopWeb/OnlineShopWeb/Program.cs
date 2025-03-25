using CouponCache;
using GrpcAdapter;
using NServiceBus;
using OnlineShopWeb.Authentication;
using OnlineShopWeb.Configuration;
using OnlineShopWeb.Misc;
using ProductCouponAdapter;
using Serilog;
using TransactionAdapter;
using UserAdapter;

var builder = WebApplication.CreateBuilder(args);
IEndpointInstance endpointInstance = null;

try
{
    var bootstrapLoggingConfiguration = new LoggerConfiguration()
        .WriteTo.File("Logs/OnlineShopWeb_Fatal.log");
    Log.Logger = bootstrapLoggingConfiguration.CreateBootstrapLogger();

    // Add services to the container.
    builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

    builder.Services.ConfigureAuthentication();
    builder.Services.ConfigureLogging(builder.Configuration, builder.Host);
    builder.Services.ConfigureUserAdapter(builder.Configuration);
    builder.Services.ConfigureProductCouponAdapter(builder.Configuration);
    builder.Services.ConfigureTransactionAdapter(builder.Configuration);

    ////TODO use bind to unify options
    //builder.Services.Configure<ApiClientOptions>(builder.Configuration.GetSection("ApiClientOptions")).
    //    Configure<ApiTransactionOptions>(builder.Configuration.GetSection("ApiTransaction"));

    // If i bind the above todo there will be problems here
    //TODO AUSLAGERN WIE USER, gemeinsam mit den options
    builder.Services.AddScoped<Utility.Misc.IHttpClientWrapper, Utility.Misc.HttpClientWrapper>().
        AddGrpcAdapter("TransactionAdapter", builder.Configuration).
        AddApplication();

    //background service
    builder.Services.AddHostedService<CacheBackgroundService>();
    builder.Services.AddCache();

    endpointInstance = await NServiceBusConfiguration.ConfigureNServiceBus(
        builder.Services,
        builder.Configuration);

    var app = builder.Build();

    app.UseMiddleware<MiddlewareCustomExceptionHandling>();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

        //app.UseHsts();
    }


    if (app.Environment.IsDevelopment())
    {
        //TODO
        //
        //app.UseHttpsRedirection();
    }

    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers().RequireAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.UseSerilogRequestLogging();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Error during Start OnlineShopWeb");
}
finally
{
    if (endpointInstance is not null)
    {
        await endpointInstance.Stop()
        .ConfigureAwait(false);
        //TODO in microsoft learn look up ConfigureAwait
    }
    Log.CloseAndFlush();
}
