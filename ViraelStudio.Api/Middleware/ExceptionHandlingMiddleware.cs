using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ViraelStudio.Api.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (DbUpdateConcurrencyException)
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;

                await context.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Concurrency conflict",
                    Detail = "The product was modified by another user. Please reload it and try again."
                });
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred."
                });
            
            }
        }
    }
}
