using E_Learning.GraduationProject.APIs.Errors;
using System.Text.Json;

namespace E_Learning.GraduationProject.APIs.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(Exception ex) 
            {
                logger.LogError(ex, ex.Message);

                var response = env.IsDevelopment() ? new ApiExceptionResponse(ex.Message, ex?.StackTrace?.ToString()) // for Developers (front and back
                                                   : new ApiExceptionResponse(ex.Message); // for app user

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);

            }
        }
    }
}
