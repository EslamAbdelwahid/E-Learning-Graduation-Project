using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Repository.Data.Context;
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

                if(concepts is not null && concepts.Count() > 0 )
                {
                    await _context.LanguageConcepts.AddRangeAsync(concepts);
                    await _context.SaveChangesAsync();
                }

            }



        }
    }
}
