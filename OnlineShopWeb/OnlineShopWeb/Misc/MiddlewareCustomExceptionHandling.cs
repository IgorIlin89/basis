using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShopWeb.Domain.Exceptions;
using OnlineShopWeb.TransferObjects.Dtos;
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
            // TODO redirect to a site with error message 
            _logger.LogWarning(domainException.Message);
        }
        catch (ApiException apiException)
        {
            _logger.LogWarning(apiException.Message);
        }
        catch (Exception exception)
        {
            
            _logger.LogWarning(exception.Message);
            context.Response.Clear();
            //context.Response.Redirect("/error/errorview");
            await context.Response.SendFileAsync("/Views/Shared/DefaultErrorPage.cshtml");
        }
    }

    // TODO invoke without async implement, check with debugger

}
