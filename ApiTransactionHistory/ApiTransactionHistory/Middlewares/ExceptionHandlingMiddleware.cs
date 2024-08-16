using ApiTransactionHistory.Domain.Dtos;
using ApiTransactionHistory.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace ApiTransactionHistory.Middlewares;

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
        catch (NotFoundException notFoundException)
        {
            context.Response.Clear();

            var actionResult = new ObjectResult(new ErrorDto
            {
                StatusCode = ErrorStatusCodeDto.NotFound,
                Message = notFoundException.Message
            });

            actionResult.StatusCode = (int)HttpStatusCode.BadRequest;

            await actionResult.ExecuteResultAsync(new ActionContext
            {
                HttpContext = context
            });

            _logger.LogWarning(notFoundException.Message);
        }
        catch (Exception exception)
        {
            context.Response.Clear();

            var actionResult = new ObjectResult(new ErrorDto
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
}
