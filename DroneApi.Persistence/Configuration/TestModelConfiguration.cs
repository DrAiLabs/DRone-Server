using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DroneApi.Core.Entities;
namespace DroneApi.Persistence.Configuration
{
    internal sealed class TestModelConfiguration : IEntityTypeConfiguration<TestModel>
    {
        public void Configure(EntityTypeBuilder<TestModel> builder)
        {
            builder.ToTable("TestModels");
            builder.Property(m => m.Name).IsRequired().HasMaxLength(10);
            builder.HasData(
            new TestModel
            {
                Id = Guid.NewGuid(),
                Name = "test",
            });
        }
    }
}
