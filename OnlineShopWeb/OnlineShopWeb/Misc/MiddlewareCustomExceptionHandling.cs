using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace OnlineShopWeb.Misc;

public class MiddlewareCustomExceptionHandling
{
    [TempData]
    public string ExceptionFromMiddleware { get; set; }
    private readonly RequestDelegate _next;

    public MiddlewareCustomExceptionHandling(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            //ExceptionFromMiddleware = exception.InnerException.ToString();
            //context.Session.SetString("ExceptionFromMiddleware", exception.Message);
            //context.Response.Redirect("unexpectederror");
            // TODO LOGGING later
            //Results.BadRequest();
        }
    }

    // TODO invoke without async implement, check with debugger

}
