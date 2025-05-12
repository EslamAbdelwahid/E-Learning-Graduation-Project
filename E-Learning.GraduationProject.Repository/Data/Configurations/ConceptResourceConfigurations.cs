using E_Learning.GraduationProject.Core.Entities;
using E_Learning.GraduationProject.Core.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Repository.Data.Configurations
{
    public class ConceptResourceConfigurations : IEntityTypeConfiguration<ConceptResource>
    {
        public void Configure(EntityTypeBuilder<ConceptResource> builder)
        {

          


            builder.Property(CR => CR.ResourceType)
                   .HasConversion(
                    // → It saves the enum as its name (a string) in the database
                    RT => RT.ToString(),
                    // → It reads the string from DB and converts it back into the enum
                    RT => (ResourceType)Enum.Parse(typeof(ResourceType), RT)
                    );
           
        }
    }
}
