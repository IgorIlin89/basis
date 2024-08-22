using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
using NServiceBus;
using OnlineShopWeb.Adapters;
using OnlineShopWeb.Adapters.Interfaces;
using OnlineShopWeb.Misc;
using OnlineShopWeb.NServiceBus.Messages;
using Serilog;

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
    })
.AddSessionStateTempDataProvider();

    builder.Services.AddSession();

    builder.Services.Configure<HttpClientWrapperOptions>("ApiClientOptions",
        builder.Configuration.GetSection("ApiClientOptions")).
        Configure<HttpClientWrapperOptions>("ApiUserClientOptions",
        builder.Configuration.GetSection("ApiUserClientOptions")).
        Configure<HttpClientWrapperOptions>("ApiCouponProductClientOptions",
        builder.Configuration.GetSection("ApiCouponProductClientOptions")).
        Configure<HttpClientWrapperOptions>("ApiTransactionHistory",
        builder.Configuration.GetSection("ApiTransactionHistory"));

    builder.Services.AddScoped<IHttpClientWrapper, HttpClientWrapper>().
        AddScoped<IUserAdapter, UserAdapter>().
        AddScoped<IProductCouponAdapter, ProductCouponAdapter>().
        AddScoped<ITransactionHistoryAdapter, TransactionHistoryAdapter>();

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
            options.LoginPath = "/Login/SignIn/";
            options.AccessDeniedPath = "/Error/Forbidden/";
        });


    builder.Services.AddAuthorization(o =>
    {
        o.AddPolicy(AuthorizeControllerModelConvention.PolicyName, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
    });

    builder.Services.AddMvc(options =>
    {
        options.Conventions.Add(new AuthorizeControllerModelConvention());
    });

    var loggingConfiguration = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProcessId()
        .Enrich.WithProcessName()
        .Enrich.WithMachineName();

    var logger = loggingConfiguration.CreateLogger();
    builder.Host.UseSerilog(logger);

    //NServiceBus
    //This is name in Database
    var endpointConfiguration = new EndpointConfiguration("OnlineShopWeb");
    endpointConfiguration.SendFailedMessagesTo("error");
    endpointConfiguration.AuditProcessedMessagesTo("audit");
    endpointConfiguration.EnableInstallers();

    // Choose JSON to serialize and deserialize messages
    endpointConfiguration.UseSerialization<NServiceBus.SystemJsonSerializer>();

    var nserviceBusConnectionString = builder.Configuration.GetConnectionString("NServiceBus");

    var transportConfig = new NServiceBus.SqlServerTransport(nserviceBusConnectionString)
    {
        DefaultSchema = "dbo",
        TransportTransactionMode = TransportTransactionMode.SendsAtomicWithReceive,
        Subscriptions =
    {
        CacheInvalidationPeriod = TimeSpan.FromMinutes(1),
        SubscriptionTableName = new NServiceBus.Transport.SqlServer.SubscriptionTableName(
            table: "Subscriptions",
            schema: "dbo")
    }
    };

    transportConfig.SchemaAndCatalog.UseSchemaForQueue("error", "dbo");
    transportConfig.SchemaAndCatalog.UseSchemaForQueue("audit", "dbo");

    var transport = endpointConfiguration.UseTransport<SqlServerTransport>(transportConfig);
    transport.RouteToEndpoint(typeof(TestCommand), "ApiTransaction");

    //persistence
    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
    dialect.Schema("dbo");
    persistence.ConnectionBuilder(() => new SqlConnection(nserviceBusConnectionString));
    persistence.TablePrefix("");

    //await SqlServerHelper.CreateSchema(nserviceBusConnectionString, "dbo");

    var endpointContainer = EndpointWithExternallyManagedContainer.Create(endpointConfiguration, builder.Services);
    endpointInstance = await endpointContainer.Start(builder.Services.BuildServiceProvider());

    //End NServiceBus

    var app = builder.Build();

    app.UseMiddleware<MiddlewareCustomExceptionHandling>();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSession();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Product}/{action=Index}/{id?}");

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
