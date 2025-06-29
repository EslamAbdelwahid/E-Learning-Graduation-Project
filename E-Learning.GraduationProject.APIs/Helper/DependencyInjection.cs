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
using E_Learning.GraduationProject.Core.Mapping.Roles;
using E_Learning.GraduationProject.Core.Mapping.Instructors;
using E_Learning.GraduationProject.Core.Mapping.Students;
using E_Learning.GraduationProject.Core.Mapping.StudentProgresses;
using E_Learning.GraduationProject.Core.Mapping.Addresses;
using E_Learning.GraduationProject.Core.Mapping.Courses;
using E_Learning.GraduationProject.Core.Repository.Contract;
using E_Learning.GraduationProject.Repository.Repository;
using StackExchange.Redis;
using E_Learning.GraduationProject.Core.Mapping.Baskets;
using E_Learning.GraduationProject.Core.Mapping.Orders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using E_Learning.GraduationProject.Core.Mapping.Contacts;


namespace E_Learning.GraduationProject.APIs.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInService();
            services.AddSwaggerService();
            services.AddCorsService();
            services.AddUserDefinedService();
            services.AddDbContextService(configuration);
            services.AddAutoMapperService();
            services.ConfigureInvalidModelStateRespnoseService();
            services.AddAuthenticationService(configuration);
            services.AddRedisService(configuration);
            services.AddGoogleAuthentication(configuration);
            return services;
        }
        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                // this options allow user to write strings in ResourceType Enum instead of integers
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.WriteIndented = false; 
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
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStepResourceService, StepResourceService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IInstructorService, InstructorService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IContactUsService, ContactUsService>();



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
            services.AddAutoMapper(M => M.AddProfile(new RolesProfile()));
            services.AddAutoMapper(M => M.AddProfile(new InstructorProfile()));
            services.AddAutoMapper(M => M.AddProfile(new AddressProfile()));
            services.AddAutoMapper(M => M.AddProfile(new StudentProfile()));
            services.AddAutoMapper(M => M.AddProfile(new StudentProgressProfile()));
            services.AddAutoMapper(M => M.AddProfile(new CourseProfile()));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            services.AddAutoMapper(M => M.AddProfile(new OrderProfile()));
            services.AddAutoMapper(M => M.AddProfile(new ContactUsProfile()));
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
                // Your existing TokenValidationParameters configuration stays the same  
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

                // Add this new event handler to read tokens from cookies  
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // First check if token is in Authorization header (existing behavior)  
                        var token = context.Request.Headers["Authorization"]
                            .FirstOrDefault()?.Split(" ").Last();

                        // If no token in header, check cookies  
                        if (string.IsNullOrEmpty(token))
                        {
                            token = context.Request.Cookies["authToken"];
                        }

                        // Set the token for validation  
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

        private static IServiceCollection AddCorsService(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "https://edulearningplatform.netlify.app")  
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
            return services;
        }

        private static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(connection);
            });
            return services;
        }

        public static IServiceCollection AddGoogleAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"];
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                options.CallbackPath = "/google-login";

            });

            return services;
        }


    }
}
