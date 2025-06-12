using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Entities.Identity;
using E_Learning.GraduationProject.Repository.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Repository.Data
{
    public static class E_LearningDbContextSeed
    {

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Student", "Instructor" };
            foreach(var role in roles)
            {
                if(! await roleManager.RoleExistsAsync(role))
                {
                   await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            
        }
        public static async Task SeedUsersAsync(
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration)
        {
            if (!userManager.Users.Any())
            {
                var seededUsersSection = configuration.GetSection("SeededUsers");

                foreach (var userSection in seededUsersSection.GetChildren())
                {
                    var user = new ApplicationUser
                    {
                        UserName = userSection["Username"],
                        Email = userSection["Email"],
                        FirstName = userSection["FirstName"],
                        LastName = userSection["LastName"],
                        PhoneNumber = userSection["Phone"],
                        Address = new Address
                        {
                            Street = userSection["Address:Street"],
                            City = userSection["Address:City"],
                            Country = userSection["Address:Country"]
                        },
                        EmailConfirmed = true
                    };

                    var password = userSection["Password"];
                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        var roles = userSection.GetSection("Roles").GetChildren()
                            .Select(x => x.Value).ToArray();
                        await userManager.AddToRolesAsync(user, roles);
                    }
                    else
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        throw new Exception($"Failed to create user {userSection["Username"]}: {errors}");
                    }
                }
            }
        }

        public static async Task SeedAsync(AppDbContext _context)
        {

            // Programming Languages
            if (_context.ProgrammingLanguages.Count() == 0)// check if table is empty (not seeded before)
            {
                // read file 
                var languageData = File.ReadAllText(@"..\E-Learning.GraduationProject.Repository\Data\DataSeed\ProgrammingLanguages.json"); // default directory at (API) so we took step back 

                //Deserialize convert json into List<T> (ProgrammingLanguage)
                var languages = JsonSerializer.Deserialize<List<ProgrammingLanguage>>(languageData);

                if (languages is not null && languages.Count() > 0)
                {
                    await _context.ProgrammingLanguages.AddRangeAsync(languages);
                    await _context.SaveChangesAsync();
                }

            }

            // Language Concepts
            if (_context.LanguageConcepts.Count() == 0)
            {
                //Read the file 
                var conceptData = File.ReadAllText(@"..\E-Learning.GraduationProject.Repository\Data\DataSeed\LanguagesConcepts.json");

                //Deserialize
                var concepts = JsonSerializer.Deserialize<List<LanguageConcept>>(conceptData);

                if (concepts is not null && concepts.Count() > 0)
                {
                    await _context.LanguageConcepts.AddRangeAsync(concepts);
                    await _context.SaveChangesAsync();
                }

            }
        }

        public static async Task SeedTracksAsync(AppDbContext _context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // This makes it ignore case mismatches
            };

            if (!_context.Tracks.Any())
            {
                var tracksData = await File.ReadAllTextAsync(@"..\E-Learning.GraduationProject.Repository\Data\DataSeed\Tracks\Tracks.json");

                var tracks = JsonSerializer.Deserialize<List<Track>>(tracksData, options);

                if (tracks != null && tracks.Count > 0)
                {
                    await _context.Tracks.AddRangeAsync(tracks);
                    await _context.SaveChangesAsync();
                }
            }

            if (!_context.TrackSteps.Any())
            {
                var tracksStepsData = await File.ReadAllTextAsync(@"..\E-Learning.GraduationProject.Repository\Data\DataSeed\Tracks\AllTracksSteps.json");

                var trackSteps = JsonSerializer.Deserialize<List<TrackStep>>(tracksStepsData, options);

                if (trackSteps != null && trackSteps.Count > 0)
                {
                    await _context.TrackSteps.AddRangeAsync(trackSteps);
                    await _context.SaveChangesAsync();
                }
            }

            if (!_context.StepResources.Any())
            {
                var stepResourcesData = await File.ReadAllTextAsync(@"..\E-Learning.GraduationProject.Repository\Data\DataSeed\Tracks\AllResources.json");

                var stepResources = JsonSerializer.Deserialize<List<StepResource>>(stepResourcesData, options);

                if (stepResources != null && stepResources.Count > 0)
                {
                    await _context.StepResources.AddRangeAsync(stepResources);
                    await _context.SaveChangesAsync();
                }
            }

        }

    }
}
