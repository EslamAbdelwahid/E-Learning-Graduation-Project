using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Repository.Data.Configurations
{
    public class ProgrammingLanguageConfigurations : IEntityTypeConfiguration<ProgrammingLanguage>
    {
        public void Configure(EntityTypeBuilder<ProgrammingLanguage> builder)
        {
            builder.Property(PL => PL.Difficulty)
                .HasConversion(
                //to
                D => D.ToString(),
                //from
                D => (DifficultyLevel)Enum.Parse(typeof(DifficultyLevel), D)
                );
        }
    }
}
