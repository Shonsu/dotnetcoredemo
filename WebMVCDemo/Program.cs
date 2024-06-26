﻿using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebMVCDemo.Security;
using WebMVCDemo.Services;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddDbContext<AppDbContext>();
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanAccessEmployee",
        policyBuilder => policyBuilder.AddRequirements(new CanAccessEmployeeRequirement()));
    options.AddPolicy("IsAdmin",
        polcyBuilder => polcyBuilder.RequireClaim("IsAdmin", "True"));
});
builder.Services.AddScoped<IAuthorizationHandler, CanAccessEmployeeHandler>();

builder.Services.ConfigureApplicationCookie(options =>
options.AccessDeniedPath = "/Account/AccessDenied");

builder.Services.AddControllers();
System.Console.WriteLine(AppContext.BaseDirectory);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home1/Error");
}
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home1}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "two",
    pattern: "Two/{id1:int}/{id2:int}",
    defaults: new { controller = "Employee", action = "Two" });
app.MapControllerRoute(
    name: "wiki",
    pattern: "Wiki/{*path}",
    defaults: new { controller = "Wiki", action = "Index" });

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
    options.RoutePrefix = "swagger"; // Set the Swagger UI at the root URL
});

app.Run();

