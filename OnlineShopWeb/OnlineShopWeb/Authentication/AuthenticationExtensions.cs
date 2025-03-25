using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace OnlineShopWeb.Authentication;

public static class AuthenticationExtensions
{
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
        })
        .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
        {
            options.Authority = "https://localhost:7073"; //TODO appsettings

            options.ClientId = "aspnetcoreweb";
            options.ClientSecret = "aspnetwebsecret";
            options.ResponseType = "code";

            //options.SaveTokens = true;

            options.Scope.Clear();
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("offline_access");
            options.Scope.Add("sampleaspnetscope");

            options.MapInboundClaims = false; // Don't rename claim types
            options.GetClaimsFromUserInfoEndpoint = true;
        });

        return services;
    }
}