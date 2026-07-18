using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure EF Core with SQLite
builder.Services.AddDbContext<SistemaGestaoLar.Api.Data.ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=database.db"));

// Register generic repository and services
builder.Services.AddScoped(typeof(SistemaGestaoLar.Api.Services.IGenericRepository<>), typeof(SistemaGestaoLar.Api.Services.GenericRepository<>));
builder.Services.AddScoped<SistemaGestaoLar.Api.Services.MoradorService>();
builder.Services.AddScoped<SistemaGestaoLar.Api.Services.AjudanteService>();
builder.Services.AddScoped<SistemaGestaoLar.Api.Services.GrupoService>();
builder.Services.AddScoped<SistemaGestaoLar.Api.Services.TicketDiarioService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Front", policy =>
        policy.WithOrigins("https://localhost:7060", "http://localhost:5105")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Sistema Gestão Lar API";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseCors("Front");
app.UseHttpsRedirection();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
