using EmailProviderSystem.Services.Interfaces;
using EmailProviderSystem.Web.APIs.Extenstions;
using EmailProviderSystem.Data.Database.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Secure Apis in swager
builder.Services.AddSwagerService();
// Add Authentication service
builder.Services.AddAuthenticationService(builder.Configuration);

// Register services
builder.Services.AddAppServices(builder.Configuration);

if (builder.Configuration["StoringDataType"] == "Database")
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
