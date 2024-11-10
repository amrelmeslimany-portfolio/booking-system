using System;
using Microsoft.AspNetCore.Mvc;

namespace api.Config.Exceptions
{
    public class AppExceptionMiddleware(
        RequestDelegate _next,
        ILogger<AppExceptionMiddleware> _logger
    )
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Trace From Middleware {ex.StackTrace} ");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(
                    new ProblemDetails
                    {
                        Title = ex.Message,
                        Status = context.Response.StatusCode,
                        Instance = $"{context.Request.Method} {context.Request.Path}",
                    }
                );
            }
        }
    }

    public static class AppExceptionExtensions
    {
        public static IApplicationBuilder UseCustomAppException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AppExceptionMiddleware>();
        }
    }
}
