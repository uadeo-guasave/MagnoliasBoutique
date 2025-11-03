using Microsoft.EntityFrameworkCore;

namespace MagnoliaDB;

public class SqliteDbContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Prenda> Prendas { get; set; }
    
    // Constructor por defecto: permite que herramientas de diseño o código existente
    // creen el contexto y usen la configuración por defecto de OnConfiguring.
    public SqliteDbContext() { }

    // Constructor para pruebas/DI: permite inyectar opciones (por ejemplo, SQLite en memoria o InMemory)
    public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Si ya viene configurado (p. ej., desde pruebas o DI), no toques la configuración.
        if (optionsBuilder.IsConfigured)
        {
            base.OnConfiguring(optionsBuilder);
            return;
        }

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
