using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Infrastructure.Shared.Models;

namespace Infrastructure.Shared.Middleware
{ 
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
       
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
 
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
 
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (statusCode, message) = ex switch
            {
                ValidationException => (HttpStatusCode.BadRequest, "Validation failed."),
                ArgumentNullException => (HttpStatusCode.BadRequest, "A required argument was null."),
                ArgumentException => (HttpStatusCode.BadRequest, "Invalid argument provided."),
                KeyNotFoundException => (HttpStatusCode.NotFound, "The requested resource was not found."),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Unauthorized access."),
                AuthenticationException => (HttpStatusCode.Unauthorized, "Invalid credentials."),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
            };
           
            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)statusCode,
                Message = message,
                TraceId = context.TraceIdentifier,
                Details = ex.Message
            };
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
