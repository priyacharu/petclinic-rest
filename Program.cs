using Microsoft.EntityFrameworkCore;
using PetClinicRest.Data;
using AutoMapper;
using PetClinicRest.Mappings;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<PetClinicDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Seed the database on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PetClinicDbContext>();
    DbInitializer.Initialize(dbContext);
}

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "PetClinic REST API V1");
    options.RoutePrefix = "swagger";
    options.DefaultModelsExpandDepth(1);
});

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

// Add API prefix routing - optional redirect
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/" && context.Request.Method == "GET")
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("PetClinic REST API - Use /swagger/index.html for API documentation");
        return;
    }
    await next();
});

app.Run();
