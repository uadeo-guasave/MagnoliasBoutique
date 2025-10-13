using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagnoliaDB;

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
    public decimal Costo { get; set; }

    [Required]
    public decimal PrecioDeRenta { get; set; }

    [Required]
    public DateOnly FechaDeAlta { get; set; }

    [Required]
    public int CategoriaId { get; set; }
    public string? ImagenUrl { get; set; }

    // EFCore
    [NotMapped]
    public Categoria? Categoria { get; set; }
}
