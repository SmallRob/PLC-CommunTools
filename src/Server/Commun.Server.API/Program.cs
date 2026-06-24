using Microsoft.EntityFrameworkCore;
using Commun.Data;
using Commun.Data.Repositories;
using Commun.Protocols;
using Commun.Server.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CommunDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=commun.db"));

builder.Services.AddScoped<DeviceRepository>();

builder.Services.AddSingleton<ProtocolPluginManager>(sp =>
{
    var pluginManager = new ProtocolPluginManager("plugins");
    pluginManager.LoadPlugins();
    return pluginManager;
});

builder.Services.AddScoped<DeviceService>();
builder.Services.AddSingleton<ProtocolService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CommunDbContext>();
    context.Database.EnsureCreated();
}

app.Run();

public partial class Program { }
