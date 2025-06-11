using E_Learning.GraduationProject.Core.Entities.Instructors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Repository.Data.Configurations
{
    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(C => C.Price).HasColumnType("decimal(18,2)");

            // Instructor
            builder.HasOne(C => C.Instructor)
                   .WithMany(I => I.Courses)
                   .HasForeignKey(C => C.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent instructor deletion if courses exist

            // programming Language
            builder.HasOne(C => C.ProgrammingLanguage)
                   .WithMany(PL => PL.Courses)
                   .HasForeignKey(C => C.ProgrammingLanguageId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent language deletion if used in courses

            // Track
            builder.HasOne(C => C.Track)
                   .WithMany(ST => ST.Courses)
                   .HasForeignKey(C => C.TrackId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent Track deletion if used in courses

            // Student Progress
            builder.HasMany(C => C.StudentProgresses)
                   .WithOne(SP => SP.Course)
                   .HasForeignKey(SP => SP.CourseId )
                   .OnDelete(DeleteBehavior.Restrict); // Prevent course deletion if progress exists
        }
    }
}
