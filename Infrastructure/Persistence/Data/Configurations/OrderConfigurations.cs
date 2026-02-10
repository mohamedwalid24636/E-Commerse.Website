using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(8,2)");
            builder.HasMany(O => O.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(O=>O.DeliveryMethod)
                .WithMany()
                .HasForeignKey(O => O.DelivaryMethodId);
            builder.OwnsOne(O => O.ShipToAddress);



        }
    }
}
