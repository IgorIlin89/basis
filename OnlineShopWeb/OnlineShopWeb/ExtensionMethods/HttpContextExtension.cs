using OnlineShopWeb.TransferObjects.Models.ListModels;
using System.Security.Claims;
using System.Text.Json;

namespace OnlineShopWeb.ExtensionMethods;

public static class HttpContextExtension
{
    public static int Name(this HttpContext httpcontext)
    {
        return Int32.Parse(httpcontext.User.Identity.Name);
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

    public static void AppendShoppingCart(this HttpContext httpcontext, ShoppingCartListModel model)
    {
        httpcontext.Response.Cookies.Append("ShoppingCartListModel", JsonSerializer.Serialize(model));
    }
}