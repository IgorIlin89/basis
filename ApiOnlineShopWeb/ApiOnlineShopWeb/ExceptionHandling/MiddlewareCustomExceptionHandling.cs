using ApiOnlineShopWeb.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiOnlineShopWeb.Dtos;

namespace ApiOnlineShopWeb.ExceptionHandling;

public class MiddlewareCustomExceptionHandling
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MiddlewareCustomExceptionHandling> _logger;
    public MiddlewareCustomExceptionHandling(RequestDelegate next,
        ILogger<MiddlewareCustomExceptionHandling> logger)
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
        catch(UserExistsException userExistsException)
        {
            context.Response.Clear();
            //TODO send to client ErrorCode and ErrorMessage as an object
            // errorDto, int code, string message
            // object result
            var actionResult = new ObjectResult(new ErrorDto()
            {
                StatusCode = userExistsException,
                Message = userExistsException.Message
            });

            actionResult.StatusCode = (int)HttpStatusCode.BadRequest;

            await actionResult.ExecuteResultAsync(new ActionContext
            {
                HttpContext = context
            });

            _logger.LogWarning(userExistsException.Message);

            // var response = new UserExistsExceptionDto (userExistsException.ErrorCode, userExistsException.Message)
            // _next(response)
        }
        catch (Exception exception)
        {
            // TODO LOGGING later

            //context.Response.Redirect("/UnexpectedError"); // this is controller name unexpected error
        }
    }

    // TODO invoke without async implement, check with debugger
}