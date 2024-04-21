using System.Security.Claims;

namespace ExtensionMethods;

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
}