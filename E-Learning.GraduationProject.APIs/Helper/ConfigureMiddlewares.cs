using E_Learning.GraduationProject.APIs.Middlewares;
using E_Learning.GraduationProject.Core.Entities.Identity;
using E_Learning.GraduationProject.Repository.Data;
using E_Learning.GraduationProject.Repository.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var configuration = services.GetRequiredService<IConfiguration>();

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var logger = loggerFactory.CreateLogger<Program>();

            try
            {
                await context.Database.MigrateAsync();

                await E_LearningDbContextSeed.SeedRolesAsync(roleManager);

                await E_LearningDbContextSeed.SeedUsersAsync(userManager, configuration);

                // seeding data
                await E_LearningDbContextSeed.SeedProgrammingLanguagesAsync(context);
                await E_LearningDbContextSeed.SeedTracksAsync(context);

                logger.LogInformation("Database migration and seeding completed successfully");
            }
            catch (Exception ex)
            {
                
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
            app.UseCors("AllowFrontend");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            return app;
        }
    }
}
