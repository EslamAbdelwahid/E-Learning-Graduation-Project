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
using E_Learning.GraduationProject.Core.Mapping.PractiseProblems;
using E_Learning.GraduationProject.Core.Mapping.TrackSteps;
using E_Learning.GraduationProject.Core.Mapping.StepResources;
using System.Text.Json.Serialization;
using E_Learning.GraduationProject.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using E_Learning.GraduationProject.Core.Mapping.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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
            services.AddAuthenticationService(configuration);
            return services;
        }
        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                // this options allow user to write strings in ResourceType Enum instead of integers
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

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
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IProgrammingLanguageService, ProgrammingLanguageService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<ILanguageConceptService, LanguageConceptService>();
            services.AddScoped<IPractiseProblemService, PractiseProblemService>();
            services.AddScoped<ITrackStepService, TrackStepService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IStepResourceService, StepResourceService>();




            return services;
        }
        private static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();    

            return services;
        }
        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            // allow Dependency injection for all identity services
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        private static IServiceCollection AddAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(M => M.AddProfile(new ConceptResourceProfile()));
            services.AddAutoMapper(M => M.AddProfile(new ProgrammingLanguageProfile()));
            services.AddAutoMapper(M => M.AddProfile(new TrackProfile()));
            services.AddAutoMapper(M => M.AddProfile(new LanguageConceptProfile()));
            services.AddAutoMapper(M => M.AddProfile(new PractiseProbemProfile()));
            services.AddAutoMapper(M => M.AddProfile(new TrackStepProfile()));
            services.AddAutoMapper(M => M.AddProfile(new StepResourceProfile()));
            services.AddAutoMapper(M => M.AddProfile(new AuthProfile()));
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

        private static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // Configure how incoming JWT tokens are validated.
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,

                    ValidIssuer = configuration["JWT:Issuer"],

                    ValidateAudience = true,

                    ValidAudience = configuration["JWT:Audience"],

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(

                        Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])
                    )
                };
            });

            return services;
        }
    }
}
