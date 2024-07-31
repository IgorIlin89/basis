using Microsoft.AspNetCore.Authentication.Cookies;
using OnlineShopWeb.Misc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

try
{
    var bootstrapLoggingConfiguration = new LoggerConfiguration()
        .WriteTo.File("OnlineShopWeb_Fatal.log");
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

    builder.Services.Configure<HttpClientWrapperOptions>(
        builder.Configuration.GetSection(HttpClientWrapperOptions.SectionName));

    builder.Services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();

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
    Log.CloseAndFlush();
}
