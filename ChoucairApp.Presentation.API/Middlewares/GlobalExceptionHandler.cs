using ChoucairApp.Core.Application.Responses;
using Microsoft.AspNetCore.Diagnostics;

namespace ChoucairApp.Presentation.API.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            string title = "";
            title = exception.Message;

            var data = new
            {
                Instance = httpContext.Request.Path,
                Title = title,
                Status = httpContext.Response.StatusCode
            };

            await httpContext.Response.WriteAsJsonAsync(Result<object>.Success(data), cancellationToken).ConfigureAwait(false);
            return true;
        }
    }
}
