using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace OnlineShopWeb.Misc;

public class MiddlewareCustomExceptionHandling
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MiddlewareCustomExceptionHandling> _logger;

    public MiddlewareCustomExceptionHandling(RequestDelegate next
        , ILogger<MiddlewareCustomExceptionHandling> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException domainException)
        {
            _logger.LogWarning(domainException.Message);
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception.Message);
            //ExceptionFromMiddleware = exception.InnerException.ToString();
            //context.Session.SetString("ExceptionFromMiddleware", exception.Message);
            //context.Response.Redirect("unexpectederror");
            // TODO LOGGING later
            //Results.BadRequest();
        }
    }

    // TODO invoke without async implement, check with debugger

}
