using System.Text.Json;

namespace ViraelStudio.Api.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    message = "An unexpected error occurred."
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            
            }
        }
    }
}
