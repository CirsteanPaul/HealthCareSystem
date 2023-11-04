using System.Net;
using System.Text.Json;
using Healthcare.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Middleware;

    internal class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred: {Message}", ex.Message);

                await HandleExceptionAsync(httpContext, ex);
            }
        }
        
        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var problemDetails = new ProblemDetails
            {
                Title = DomainErrors.General.InternalServerError.Code,
                Detail = DomainErrors.General.InternalServerError.Message
            };

            string response = JsonSerializer.Serialize(problemDetails, serializerOptions);

            await httpContext.Response.WriteAsync(response);
        }
    }

    /// <summary>
    /// Contains extension methods for configuring the exception handler middleware.
    /// </summary>
    internal static class ExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Configure the custom exception handler middleware.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The configured application builder.</returns>
        internal static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
