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
builder.Services.AddScoped<SistemaGestaoLar.Api.Services.ServicoStatusService>();
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

// Apply pending EF Core migrations automatically at startup.
// Skip when the host is spun up in-process by NSwag (during "dotnet build" in Release)
// to generate the OpenAPI document, otherwise the migration lock can be acquired
// concurrently by multiple processes and the build hangs waiting for it to release.
if (!app.Environment.IsEnvironment("NSwagGenerator"))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<SistemaGestaoLar.Api.Data.ApplicationDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseCors("Front");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
