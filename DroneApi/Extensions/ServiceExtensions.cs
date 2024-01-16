using DroneApi.Core.Contracts;
using DroneApi.LoggerService;
using Microsoft.EntityFrameworkCore;
using DroneApi.Persistence.Context;
using DroneApi.Core.Mappings;
using AutoMapper;
using DroneApi.Core.Mapping;
using DroneApi.Persistence.Repositories;
using DroneApi.Services;
using DroneApi.Services.Contracts;

namespace DroneApi.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlConnection"), b => b.MigrationsAssembly("DroneApi.Persistence")));

            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            var mapperConfig = new MapperConfiguration(map =>
            {
                map.AddProfile<TestModelMappingProfile>();
                map.AddProfile<OrderMappingProfile>();
            });
            
            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddSwaggerGen();
            
            //This is only for development purpose
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            return services;
        }
    }
}
