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
    public class PractiseProblemConfigurations : IEntityTypeConfiguration<PractiseProblem>
    {
        void IEntityTypeConfiguration<PractiseProblem>.Configure(EntityTypeBuilder<PractiseProblem> builder)
        {
            // platform
            builder.Property(PP => PP.PlatformName)
                   .HasConversion(
                    //to
                    PN => PN.ToString(),
                    //from
                    PN => (PlatformName)Enum.Parse(typeof(PlatformName) , PN)
                );

            // difficulty level
            builder.Property(PP => PP.ProblemDifficultyLevel)
                   .HasConversion(
                    //to
                    PDL => PDL.ToString(),
                    //from
                    PDL => (ProblemDifficultyLevel)Enum.Parse(typeof(ProblemDifficultyLevel), PDL)
                );

            // entity type
            builder.Property(PP => PP.EntityType)
                   .HasConversion(
                    //to
                    ET => ET.ToString(),    
                    //from
                    ET => (EntityType)Enum.Parse(typeof(EntityType), ET)
                );
        }
    }
}
