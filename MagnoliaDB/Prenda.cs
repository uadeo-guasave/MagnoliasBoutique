using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MagnoliaDB;

[Index(nameof(CodigoDeReferencia), IsUnique = true)]
public class Prenda
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public required string Nombre { get; set; }

    [Required]
    public required string CodigoDeReferencia { get; set; }

    [Required]
    [MaxLength(500)]
    public required string Descripcion { get; set; }

    [Required]
    public required string Talla { get; set; }

    [Required]
    public required string Color { get; set; }

    [Required]
    public required string Material { get; set; }

    [Required]
    public decimal PrecioDeRenta { get; set; }

    [Required, ForeignKey("FK_Prendas_Categorias_CategoriaId_Id")]
    public int CategoriaId { get; set; }
    public string? ImagenUrl { get; set; }

    // EFCore
    [NotMapped]
    public Categoria? Categoria { get; set; }

    // Método para obtener las N prendas más recientes (inyectando el DbContext)
    public static List<PrendaDTO> obtenerLasPrendasMasRecientes(SqliteDbContext db, int numeroDePrendas)
    {
        var prendas = db.Prendas
                        .OrderByDescending(p => p.Id)
                        .Take(numeroDePrendas)
                        .Select(p => new PrendaDTO
                        {
                            Id = p.Id,
                            Nombre = p.Nombre,
                            ImagenUrl = p.ImagenUrl
                        })
                        .ToList();
        return prendas;
    }

    // Método para guardar una prenda nueva
    public bool GuardarNuevaPrenda(SqliteDbContext db)
    {
        db.Prendas.Add(this);
        return db.SaveChanges() > 0;
    }
}
