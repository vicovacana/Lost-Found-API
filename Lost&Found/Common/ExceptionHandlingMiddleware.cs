using Microsoft.AspNetCore.Mvc;

namespace Lost_Found.Common
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var (statusCode, title) = ex switch
                {
                    NotFoundException => (StatusCodes.Status404NotFound, "Not Found"),
                    ForbiddenException => (StatusCodes.Status403Forbidden, "Forbidden"),
                    ConflictException => (StatusCodes.Status409Conflict, "Conflict"),
                    ValidationException => (StatusCodes.Status400BadRequest, "Validation Error"),
                    _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
                };

                if (statusCode == StatusCodes.Status500InternalServerError)
                {
                    _logger.LogError(ex, "Unhandled exception while processing {Method} {Path}",
                        context.Request.Method, context.Request.Path);
                }

                var problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Title = title,
                    Detail = ex.Message
                };

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
