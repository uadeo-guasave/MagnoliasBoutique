using Microsoft.EntityFrameworkCore;

namespace MagnoliaDB;

public class SqliteDbContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Prenda> Prendas { get; set; }
    public DbSet<Inventario> Inventarios { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Renta> Rentas { get; set; }
    public DbSet<DetalleRenta> DetallesRenta { get; set; }
    
    // Constructor para DI: permite inyectar opciones desde el host (WebAPI, consola, pruebas, etc.)
    public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar conversión de enum a string para EstadoRenta
        modelBuilder.Entity<Renta>()
            .Property(r => r.Estado)
            .HasConversion<string>();

        // Configurar relación Categoria -> Prenda (1:N)
        modelBuilder.Entity<Prenda>()
            .HasOne<Categoria>()
            .WithMany()
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configurar relación Prenda -> Inventario (1:N)
        modelBuilder.Entity<Inventario>()
            .HasOne<Prenda>()
            .WithMany()
            .HasForeignKey(i => i.PrendaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configurar relación Cliente -> Renta (1:N)
        modelBuilder.Entity<Renta>()
            .HasOne<Cliente>()
            .WithMany()
            .HasForeignKey(r => r.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configurar relación Renta -> DetalleRenta (1:N)
        modelBuilder.Entity<DetalleRenta>()
            .HasOne<Renta>()
            .WithMany()
            .HasForeignKey(dr => dr.RentaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurar relación Prenda -> DetalleRenta (1:N)
        modelBuilder.Entity<DetalleRenta>()
            .HasOne<Prenda>()
            .WithMany()
            .HasForeignKey(dr => dr.PrendaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
