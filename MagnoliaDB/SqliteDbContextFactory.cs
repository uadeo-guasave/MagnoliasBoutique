using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MagnoliaDB;

/// <summary>
/// Factory para crear instancias de SqliteDbContext en tiempo de diseño
/// (usado por herramientas como dotnet ef migrations add)
/// </summary>
public class SqliteDbContextFactory : IDesignTimeDbContextFactory<SqliteDbContext>
{
    public SqliteDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SqliteDbContext>();
        
        // Configuración por defecto para herramientas de diseño
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var dbDirectory = Path.Combine(baseDirectory, "Db");
        
        if (!Directory.Exists(dbDirectory))
        {
            Directory.CreateDirectory(dbDirectory);
        }
        
        var dbPath = Path.Combine(dbDirectory, "magnolia.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
        
        return new SqliteDbContext(optionsBuilder.Options);
    }
}
