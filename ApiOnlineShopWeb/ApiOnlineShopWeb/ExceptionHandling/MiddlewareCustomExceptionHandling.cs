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

            var actionResult = new ObjectResult(new ErrorDto()
            {
                StatusCode = ErrorStatusCodeDto.UserExists,
                Message = userExistsException.Message
            });

            actionResult.StatusCode = (int)HttpStatusCode.BadRequest;

            await actionResult.ExecuteResultAsync(new ActionContext
            {
                HttpContext = context
            });

            _logger.LogWarning(userExistsException.Message);
        }
        catch (CouponExistsException couponExistsException)
        {
            context.Response.Clear();

            var actionResult = new ObjectResult(new ErrorDto()
            {
                StatusCode = ErrorStatusCodeDto.CouponExists,
                Message = couponExistsException.Message
            });

            actionResult.StatusCode = (int)HttpStatusCode.BadRequest;

            await actionResult.ExecuteResultAsync(new ActionContext
            {
                HttpContext = context
            });

            _logger.LogWarning(couponExistsException.Message);
        }
        catch (Exception exception)
        {
            var actionResult = new ObjectResult(new ErrorDto()
            {
                StatusCode = ErrorStatusCodeDto.DefaultException,
                Message = exception.Message
            });

            actionResult.StatusCode = (int)HttpStatusCode.BadRequest;

            await actionResult.ExecuteResultAsync(new ActionContext
            {
                HttpContext = context
            });

            _logger.LogWarning(exception.Message);
        }
    }
    // TODO invoke without async implement, check with debugger
}