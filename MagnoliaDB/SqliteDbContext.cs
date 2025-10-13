using Microsoft.EntityFrameworkCore;

namespace MagnoliaDB;

public class SqliteDbContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Obtener el directorio base donde está el ejecutable
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var dbDirectory = Path.Combine(baseDirectory, "Db");

        // Crear el directorio si no existe
        if (!Directory.Exists(dbDirectory))
        {
            Directory.CreateDirectory(dbDirectory);
        }

        var dbPath = Path.Combine(dbDirectory, "magnolia.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
        base.OnConfiguring(optionsBuilder);
    }
}
