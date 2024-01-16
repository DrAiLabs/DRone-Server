using Microsoft.EntityFrameworkCore;
using DroneApi.Core.Entities;
using DroneApi.Persistence.Configuration;

namespace DroneApi.Persistence.Context
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TestModelConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }

        public DbSet<TestModel>? TestModel { get; set; }
        public DbSet<Order>? Order { get; set; }
    }
}
