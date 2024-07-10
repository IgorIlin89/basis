namespace ApiOnlineShopWeb.ExceptionHandling;

public class MiddlewareCustomExceptionHandling
{
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
        catch(Exception exception)
        {
            if (exception.GetType() == typeof(NullReferenceException))
            {
                await context.Response.WriteAsync("No entry in database");
                Results.BadRequest();
            }
            // TODO LOGGING later

            //context.Response.Redirect("/UnexpectedError"); // this is controller name unexpected error
        }
    }

    // TODO invoke without async implement, check with debugger
}