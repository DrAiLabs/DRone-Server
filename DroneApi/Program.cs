using NLog;
using Microsoft.AspNetCore.HttpOverrides;
using DroneApi.Web.Extensions;
using DroneApi.Core.Contracts;
using DroneApi.Web.Middlewares;
using Microsoft.AspNetCore.Mvc;
using DroneApi.Persistence.Repositories;
using DroneApi.Services.Contracts;
using DroneApi.Services;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
//var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("/nlog.config");


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
//builder.Services.ConfigureCors();
//builder.Services.ConfigureIISIntegration();
//builder.Services.ConfigureLoggerService();

builder.Services.AddControllers().AddApplicationPart(typeof(DroneApi.Presentation.AssemblyReference).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();

//With this, we are suppressing a default model state validation that is implemented
//due to the existence of the [ApiController] attribute in all API controllers

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction()) app.UseHsts();

//this is only used during development
/*if (app.Environment.IsDevelopment()) 
    app.UseDeveloperExceptionPage(); 
else app.UseHsts();*/

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
