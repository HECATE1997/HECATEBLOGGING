using IdentityService.API.Extensions;
using IdentityService.Application;
using IdentityService.Application.Extensions;
using IdentityService.Infrastructure;
using IdentityService.Infrastructure.Seed;
using IdentityService.Infrastructure.ServiceConfig;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IdentityService.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

// JWT Config
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]))
    };
});

builder.Services.AddApplicationServices();       // MediatR, use-cases
builder.Services.AddInfrastructureServices(builder.Configuration); // Identity + MSSQL
builder.Services.AddDependencyInjection();       // DI for services
builder.Services.AddApiServices();               // Swagger, controllers

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
await IdentitySeeder.SeedDefaultUserAsync(app.Services);
app.Run();
