using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Web.Data;
using WorkoutTracker.Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure HTTPS redirection
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 5003;
});

// Configure database
builder.Services.AddDbContext<WorkoutContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

// Configure CORS for Azure Static Web Apps
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAzureStaticWebApps",
        builder => builder
            .WithOrigins(
                "https://green-bay-07e299f1e.6.azurestaticapps.net",
                "http://localhost:5002",
                "https://localhost:5003"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

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

// Enable CORS
app.UseCors("AllowAzureStaticWebApps");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<WorkoutContext>();
    context.Database.EnsureCreated();
}

app.Run(); 