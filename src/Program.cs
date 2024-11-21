using Microsoft.EntityFrameworkCore;
using DynamicDbServer.src.Shared.Data;
using DynamicDbServer.src.Features.DynamicDbBase.Services;
using DynamicDbServer.src.Features.DynamicDbBase.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4200")
                     .AllowAnyHeader()
                     .AllowCredentials()
                     .AllowAnyMethod();
    });
});
#endregion

#region Logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
#endregion

// Optional DB setup
builder.Services.AddHostedService<DatabaseInitializer>();
builder.Services.AddScoped<IDynamicDbService, DynamicDbService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowAngular");
app.MapControllers();
app.Run();
