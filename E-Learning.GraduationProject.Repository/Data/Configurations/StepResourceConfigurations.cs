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
    public class StepResourceConfigurations : IEntityTypeConfiguration<StepResource>
    {
        public void Configure(EntityTypeBuilder<StepResource> builder)
        {

           


            builder.Property(SR => SR.ResourceType)
                  .HasConversion(
                   //to
                   RT => RT.ToString(),
                   //from
                   RT => (ResourceType)Enum.Parse(typeof(ResourceType),RT)
                   );

        }
    }
}
