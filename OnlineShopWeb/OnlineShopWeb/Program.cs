using CouponCache;
using GrpcAdapter;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Data.SqlClient;
using NServiceBus;
using OnlineShopWeb.Messages.V2.Events;
using OnlineShopWeb.Misc;
using Serilog;
using Utility.Misc.Options;

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

    //TODO use bind to unify options
    builder.Services.Configure<ApiClientOptions>(builder.Configuration.GetSection("ApiClientOptions")).
        Configure<ApiUserOptions>(builder.Configuration.GetSection("ApiUserClientOptions")).
        Configure<ApiCouponProductOptions>(builder.Configuration.GetSection("ApiCouponProductClientOptions")).
        //Configure<ApiCouponProductOptions>("ApiCouponProductClientOptions", builder.Configuration).
        Configure<ApiTransactionOptions>(builder.Configuration.GetSection("ApiTransaction"));

    // If i bind the above todo there will be problems here
    builder.Services.AddScoped<Utility.Misc.IHttpClientWrapper, Utility.Misc.HttpClientWrapper>().
        AddScoped<UserAdapter.IUserAdapter, UserAdapter.UserAdapter>().
        AddScoped<ProductCouponAdapter.IProductCouponAdapter, ProductCouponAdapter.ProductCouponAdapter>().
        AddScoped<TransactionAdapter.ITransactionAdapter, TransactionAdapter.TransactionAdapterHttp>().
        AddGrpcAdapter("TransactionAdapter", builder.Configuration).
        AddApplication();

    //background service
    builder.Services.AddHostedService<CacheBackgroundService>();
    builder.Services.AddCache();

    ////COOKIE AUTHENTICATION
    //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    //    .AddCookie(options =>
    //    {
    //        options.Cookie.HttpOnly = true;
    //        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    //        options.Cookie.SameSite = SameSiteMode.Lax;
    //        options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
    //        options.LoginPath = "/Login/SignIn/";
    //        options.AccessDeniedPath = "/Error/Forbidden/";
    //    });


    //builder.Services.AddAuthorization(o =>
    //{
    //    o.AddPolicy(AuthorizeControllerModelConvention.PolicyName, policy =>
    //    {
    //        policy.RequireAuthenticatedUser();
    //    });
    //});

    //builder.Services.AddMvc(options =>
    //{
    //    options.Conventions.Add(new AuthorizeControllerModelConvention());
    //});

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
    })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "https://localhost:7073";

        options.ClientId = "aspnetcoreweb";
        options.ClientSecret = "aspnetwebsecret";
        options.ResponseType = "code";

        options.SaveTokens = true;

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("offline_access");
        options.Scope.Add("sampleaspnetscope");

        options.MapInboundClaims = false; // Don't rename claim types
        options.GetClaimsFromUserInfoEndpoint = true;
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
    transport.RouteToEndpoint(typeof(AddTransactionEvent), "ApiTransaction");

    //persistence
    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
    dialect.Schema("dbo");
    persistence.ConnectionBuilder(() => new SqlConnection(nserviceBusConnectionString));
    persistence.TablePrefix("");

    //await SqlServerHelper.CreateSchema(nserviceBusConnectionString, "dbo");

    var endpointContainer = EndpointWithExternallyManagedContainer.Create(endpointConfiguration, builder.Services);
    endpointInstance = await endpointContainer.Start(builder.Services.BuildServiceProvider());

    builder.Services.AddSingleton<NServiceBus.IMessageSession>(endpointInstance);

    //End NServiceBus

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

    app.UseSession();

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
