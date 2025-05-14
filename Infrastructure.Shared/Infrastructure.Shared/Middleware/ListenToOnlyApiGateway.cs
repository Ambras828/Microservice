
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Shared.Middleware
{
    public class ListenToOnlyApiGateway
    {
        private readonly RequestDelegate _next;

        public ListenToOnlyApiGateway(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var signedHeader = context.Request.Headers["Api-Gateway"];

            if (signedHeader.FirstOrDefault() is null)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Sorry, service is unavailable");
                return;
            }

            await _next(context);
        }
    }
}
