using System.Net;
using AirbnbDiploma.Core.Exceptions;

namespace AirbnbDiploma.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            await ChangeResponseAsync(context.Response, HttpStatusCode.NotFound, ex.Message);
        }
        catch (BadRequestException ex)
        {
            await ChangeResponseAsync(context.Response, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (UnauthorizedException ex)
        {
            await ChangeResponseAsync(context.Response, HttpStatusCode.Unauthorized, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unknown error occured.");
            await ChangeResponseAsync(context.Response, HttpStatusCode.InternalServerError, "Unknown error occured. Try again later.");
        }
    }

    public async Task ChangeResponseAsync(HttpResponse response, HttpStatusCode code, string message)
    {
        response.StatusCode = (int)code;
        response.Headers.Remove("Cache-Control");
        response.ContentType = "text/plain";
        await response.WriteAsync(message);
    }
}

