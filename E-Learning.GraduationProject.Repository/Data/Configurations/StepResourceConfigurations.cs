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



            // I will store ResourceTypeString Property not enum to make our life easier
            builder.Property(e => e.ResourceTypeString)
                   .HasColumnName("ResourceType")
                   .IsRequired();

            builder.Ignore(e => e.ResourceType);

        }
    }
}
