//using Microsoft.EntityFrameworkCore.Design;
//using DroneApi.Persistence;

//using DroneApi.Persistence.Context;
//using Microsoft.EntityFrameworkCore;

//namespace DroneApi.Web.ContextFactory
//{
//    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
//    {
//        public RepositoryContext CreateDbContext(string[] args)
//        {
//            var configuration = new ConfigurationBuilder()
//            .SetBasePath(Directory.GetCurrentDirectory())
//            .AddJsonFile("appsettings.json")
//            .Build();
//            var builder = new DbContextOptionsBuilder<RepositoryContext>()
//            .UseSqlServer(configuration.GetConnectionString("SqlConnection"));
//            return new RepositoryContext(builder.Options);
//        }
//    }
//}