using E_Learning.GraduationProject.Core.Entities.Enums;
using E_Learning.GraduationProject.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Repository.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.Property(O => O.TotalAmount).HasColumnType("decimal(18,2)");

            builder.Property(O => O.Status)
                   .HasConversion(
                    // to 
                    S => S.ToString(),
                    //from
                    S => (OrderStatus)Enum.Parse(typeof(OrderStatus), S)
                    );

            builder.HasMany(O => O.OrderItems)
                   .WithOne(OI => OI.Order)
                   .HasForeignKey(OI => OI.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);   
        }
    }
}
