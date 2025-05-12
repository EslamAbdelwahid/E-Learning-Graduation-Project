
using AutoMapper;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Mapping.ConceptResources;
using E_Learning.GraduationProject.Core.Mapping.ProgrammingLanguages;
using E_Learning.GraduationProject.Core.Mapping.Tracks;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Repository;
using E_Learning.GraduationProject.Repository.Data;
using E_Learning.GraduationProject.Repository.Data.Context;
using E_Learning.GraduationProject.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.GraduationProject.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IConceptResourceService, ConceptResourceService>();
            builder.Services.AddScoped<IProgrammingLanguageService, ProgrammingLanguageService>();
            builder.Services.AddScoped<ITrackService, TrackService>();  

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });


            builder.Services.AddAutoMapper(M => M.AddProfile(new ConceptResourceProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new ProgrammingLanguageProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new TrackProfile()));

            var app = builder.Build();

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

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
