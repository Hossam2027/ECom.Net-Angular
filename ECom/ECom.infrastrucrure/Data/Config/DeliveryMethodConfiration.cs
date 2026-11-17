using ECom.Core.Entites.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.infrastrucrure.Data.Config
{
    internal class DeliveryMethodConfiration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Price).HasColumnType("decimal(18,2)");
            builder.HasData(
                new DeliveryMethod
                {
                    Id = 1,
                    Name = "DHL",
                    DeliveryTime = "Fastest",
                    Description = "Get your order in 1-2 business days",
                    Price = 10.99m
                }, new DeliveryMethod


                {
                    Id = 2,
                    Name = "UPS",
                    DeliveryTime = "Regular",
                    Description = "Get your order in 3-5 business days",
                    Price = 5.99m
                }

            );
            }
    }
}
