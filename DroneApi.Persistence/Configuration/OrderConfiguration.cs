using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DroneApi.Core.Entities;

namespace DroneApi.Persistence.Configuration
{
    internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder) 
        {
            builder.ToTable("SubmitedOrders");
            builder.Property(m => m.BusinessLocation).IsRequired().HasMaxLength(300);
            builder.Property(m => m.DropoffLocation).IsRequired().HasMaxLength(300);
            builder.HasData(
                new Order
                {
                    Id = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                    BusinessLocation = "Villa Mella-Sector14",
                    DropoffLocation = "dropoff-station-4",
                });
        }  
    }
}
