using ApiOnlineShopWeb.Domain.Exceptions;
using ApiOnlineShopWeb.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiOnlineShopWeb.ExceptionHandling;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (UserExistsException userExistsException)
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