using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MagnoliaDB;
using MagnoliaDB.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Configurar DbContext con ConnectionString o Factory por defecto
var connectionString = builder.Configuration.GetConnectionString("MagnoliaDb");
if (!string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddDbContext<SqliteDbContext>(options =>
        options.UseSqlite(connectionString));
}
else
{
    // Usar factory por defecto si no hay ConnectionString
    builder.Services.AddDbContext<SqliteDbContext>(options =>
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var dbDirectory = Path.Combine(baseDirectory, "Db");
        if (!Directory.Exists(dbDirectory))
        {
            Directory.CreateDirectory(dbDirectory);
        }
        var dbPath = Path.Combine(dbDirectory, "magnolia.db");
        options.UseSqlite($"Data Source={dbPath}");
    });
}

// Registrar servicios de negocio
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<PrendaService>();
builder.Services.AddScoped<InventarioService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<RentaService>();
builder.Services.AddScoped<DetalleRentaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/about", () =>
{
    return "Hola desde mi WebAPI con Net 10";
})
.WithName("GetAbout");

app.Run();

