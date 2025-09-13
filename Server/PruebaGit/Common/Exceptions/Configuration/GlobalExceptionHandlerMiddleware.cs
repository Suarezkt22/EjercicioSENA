using System.Net;
using System.Text.Json;

namespace GitEjercicioSENA.Common.Exceptions.Configuration;



public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Ocurrió una excepción inesperada");

        context.Response.ContentType = "application/json";


        if (exception is CustomException customException)
        {
            context.Response.StatusCode = customException.ErrorCode;

            ProblemDetail detail = new(customException.ErrorCode, customException.Message);

            return context.Response.WriteAsync(JsonSerializer.Serialize(detail));
        }

        ProblemDetail internaldetail = new((int)HttpStatusCode.InternalServerError, "Hubo un error interno inesperado intente mas tarde.");
        context.Response.StatusCode = internaldetail.StatusCode;

        return context.Response.WriteAsync(JsonSerializer.Serialize(internaldetail));
    }
}


