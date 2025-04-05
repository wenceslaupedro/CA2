using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Web.Data;
using WorkoutTracker.Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // Azure Static Web Apps uses port 8080
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Set environment based on configuration
var environment = builder.Environment;
environment.EnvironmentName = builder.Configuration["ASPNETCORE_ENVIRONMENT"] ?? "Production";

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ensure the wwwroot directory exists
var wwwrootPath = Path.Combine(app.Environment.ContentRootPath, "wwwroot");
if (!Directory.Exists(wwwrootPath))
{
    Directory.CreateDirectory(wwwrootPath);
}

// Configure for Azure Static Web Apps
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api"))
    {
        context.Response.StatusCode = 404;
        return;
    }
    await next();
});

app.Run(); 