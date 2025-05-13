using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Repository;
using E_Learning.GraduationProject.Service.Services;
using E_Learning.GraduationProject.Repository.Data.Context;
using Microsoft.EntityFrameworkCore;
using E_Learning.GraduationProject.Core.Mapping.ConceptResources;
using E_Learning.GraduationProject.Core.Mapping.ProgrammingLanguages;
using E_Learning.GraduationProject.Core.Mapping.Tracks;
using Microsoft.AspNetCore.Mvc;
using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Mapping.LanguageConcepts;

namespace E_Learning.GraduationProject.APIs.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInService();
            services.AddSwaggerService();
            services.AddUserDefinedService();
            services.AddDbContextService(configuration);
            services.AddAutoMapperService();
            services.ConfigureInvalidModelStateRespnoseService();
            return services;
        }
        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }
        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IConceptService, ConceptService>();
            services.AddScoped<IProgrammingLanguageService, ProgrammingLanguageService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<IConceptService, ConceptService>();


            return services;
        }
        private static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            return services;
        }
        private static IServiceCollection AddAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(M => M.AddProfile(new ConceptResourceProfile()));
            services.AddAutoMapper(M => M.AddProfile(new ProgrammingLanguageProfile()));
            services.AddAutoMapper(M => M.AddProfile(new TrackProfile()));
            services.AddAutoMapper(M => M.AddProfile(new LanguageConceptProfile()));

            return services;
        }
        private static IServiceCollection ConfigureInvalidModelStateRespnoseService(this IServiceCollection services)
        {
            // to add validation error response
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                            .SelectMany(p => p.Value.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToArray();
                    var response = new ApiValidationErrorResponse() { Errors = errors };

                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }
    }
}
