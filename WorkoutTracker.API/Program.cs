using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5050);
});

// Add services to the container.
builder.Services.AddControllers()
    .AddApplicationPart(typeof(WorkoutTracker.API.Controllers.AnalysisController).Assembly);

builder.Services.AddDbContext<WorkoutContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure CORS for Azure Static Web Apps
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAzureStaticWebApps",
        builder => builder
            .WithOrigins("https://green-bay-07e299f1e.6.azurestaticapps.net")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

// Enable CORS
app.UseCors("AllowAzureStaticWebApps");

app.UseAuthorization();

app.MapControllers();

app.Run(); 