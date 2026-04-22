using Microsoft.EntityFrameworkCore;
using KMCEventAPI.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("conn");
builder.Services.AddDbContext<AppDBContext>(opt => opt.UseSqlServer(conn));

// Repositories
builder.Services.AddScoped<EventRepo>();
builder.Services.AddScoped<OrganizerRepo>();
builder.Services.AddScoped<RegistrationRepo>();

// AutoMapper
builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();