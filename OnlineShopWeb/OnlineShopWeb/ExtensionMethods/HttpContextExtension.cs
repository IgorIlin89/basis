using OnlineShopWeb.Models;
using System.Security.Claims;
using System.Text.Json;

namespace OnlineShopWeb.ExtensionMethods;

public static class HttpContextExtension
{
    public static int GetUserId(this HttpContext httpcontext)
    {
        if (httpcontext.User.Identity.IsAuthenticated)
        {
            var userId = httpcontext.User.Claims.FirstOrDefault(o =>
                o.Type == "sub").Value;

            return Int32.Parse(userId);
        }

        return 0;
    }

    public static string? GivenName(this HttpContext httpcontext)
    {
        return httpcontext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value.Trim();
    }

    public static string? Surname(this HttpContext httpcontext)
    {
        return httpcontext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value.Trim();
    }

    public static string? GetShoppingCart(this HttpContext httpcontext)
    {
        return httpcontext.Request.Cookies["ShoppingCartListModel"];
    }

    public static void AppendShoppingCart(this HttpContext httpcontext, ShoppingCartModel model)
    {
        httpcontext.Response.Cookies.Append("ShoppingCartListModel", JsonSerializer.Serialize(model));
    }
}