using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Виникла необроблена помилка: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string errorType = "ServerError";
            string? details = null;

            if (exception is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
                errorType = "Unauthorized";
            }
            else if (exception is ArgumentException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                errorType = "BadRequest";
            }
            else if (exception is NotImplementedException)
            {
                statusCode = (int)HttpStatusCode.NotImplemented;
                errorType = "NotImplemented";
            }

#if DEBUG
            details = exception.StackTrace;
#endif
            var result = JsonSerializer.Serialize(new
            {
                error = exception.Message,
                type = errorType,
                status = statusCode,
                details
            });
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(result);
        }
    }
} 