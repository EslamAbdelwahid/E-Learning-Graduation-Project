
using E_Learning.GraduationProject.APIs.Helper;
using E_Learning.GraduationProject.APIs.Middlewares;
using E_Learning.GraduationProject.Core;
using E_Learning.GraduationProject.Core.Mapping.ConceptResources;
using E_Learning.GraduationProject.Core.Mapping.ProgrammingLanguages;
using E_Learning.GraduationProject.Core.Mapping.Tracks;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Repository;
using E_Learning.GraduationProject.Repository.Data;
using E_Learning.GraduationProject.Repository.Data.Context;
using E_Learning.GraduationProject.Service.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.GraduationProject.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDependency(builder.Configuration);


            var app = builder.Build();

            await app.AddMiddlewaresAsync();

            app.Run();
        }
    }
}
