using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Repository.Data.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<StudentProgress> UserProgresses { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<LanguageConcept> LanguageConcepts { get; set; }
        public DbSet<ConceptResource> ConceptResources { get; set; }
        public DbSet<PractiseProblem> PractiseProblems { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<TrackStep> TrackSteps { get; set; }
        public DbSet<StepResource> StepResources { get; set; }
        




    }
}
