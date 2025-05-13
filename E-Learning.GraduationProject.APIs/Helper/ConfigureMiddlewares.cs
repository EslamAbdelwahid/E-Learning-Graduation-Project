using E_Learning.GraduationProject.APIs.Middlewares;
using E_Learning.GraduationProject.Repository.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.GraduationProject.APIs.Helper
{
    public static class ConfigureMiddlewares
    {
        public static async Task<WebApplication> AddMiddlewaresAsync(this WebApplication app)
        {

            // create scope that has all the services
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            // get service AppDbContext
            var context = services.GetRequiredService<AppDbContext>();

            // log the exception
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await context.Database.MigrateAsync();
                //await E_LearningDbContextSeed.SeedAsync(context);

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex, "There is a problem while applying the migrations ");

            }

            app.UseMiddleware<ExceptionMiddleware>(); // Configure user-defined middleware

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthorization();


            app.MapControllers();
            return app;
        }
    }
}
