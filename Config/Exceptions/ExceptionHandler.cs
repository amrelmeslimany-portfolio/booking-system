using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace api.Config.Exceptions
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            switch (exception)
            {
                default:
                    await httpContext.Response.WriteAsJsonAsync(
                        new ProblemDetails
                        {
                            Status = StatusCodes.Status500InternalServerError,
                            Type = exception.GetType().Name,
                            Title = exception.Message,
                            Detail = exception.StackTrace,
                            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                        },
                        cancellationToken
                    );
                    break;
            }
            return true;
        }
    }
}
