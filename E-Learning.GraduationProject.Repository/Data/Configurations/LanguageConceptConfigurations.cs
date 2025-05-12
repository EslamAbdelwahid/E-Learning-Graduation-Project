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
    public class LanguageConceptConfigurations : IEntityTypeConfiguration<LanguageConcept>
    {
        public void Configure(EntityTypeBuilder<LanguageConcept> builder)
        {

           

            builder.Property(LC => LC.DifficultyLevel)
                    .HasConversion(
                    // save as string in db 
                    DL => DL.ToString(),
                    //return from db
                    DL => (DifficultyLevel)Enum.Parse(typeof(DifficultyLevel),DL)
                );
        }
    }
}
