using System.ComponentModel.DataAnnotations;

namespace MagnoliaWebAPI10.DTOs;

public class PrendaDTO
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public required string CodigoDeReferencia { get; set; }
    public required string Descripcion { get; set; }
    public required string Talla { get; set; }
    public required string Color { get; set; }
    public required string Material { get; set; }
    public decimal PrecioDeRenta { get; set; }
    public int CategoriaId { get; set; }
    public string? ImagenUrl { get; set; }
}

public class PrendaCreateDTO
{
    [Required(ErrorMessage = "El nombre es requerido")]
    [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    public required string Nombre { get; set; }

    [Required(ErrorMessage = "El código de referencia es requerido")]
    public required string CodigoDeReferencia { get; set; }

    [Required(ErrorMessage = "La descripción es requerida")]
    [MaxLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
    public required string Descripcion { get; set; }

    [Required(ErrorMessage = "La talla es requerida")]
    public required string Talla { get; set; }

    [Required(ErrorMessage = "El color es requerido")]
    public required string Color { get; set; }

    [Required(ErrorMessage = "El material es requerido")]
    public required string Material { get; set; }

    [Required(ErrorMessage = "El precio de renta es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal PrecioDeRenta { get; set; }

    [Required(ErrorMessage = "La categoría es requerida")]
    public int CategoriaId { get; set; }

    [Url(ErrorMessage = "La URL de la imagen no es válida")]
    public string? ImagenUrl { get; set; }
}

public class PrendaUpdateDTO
{
    [Required(ErrorMessage = "El nombre es requerido")]
    [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    public required string Nombre { get; set; }

    [Required(ErrorMessage = "El código de referencia es requerido")]
    public required string CodigoDeReferencia { get; set; }

    [Required(ErrorMessage = "La descripción es requerida")]
    [MaxLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
    public required string Descripcion { get; set; }

    [Required(ErrorMessage = "La talla es requerida")]
    public required string Talla { get; set; }

    [Required(ErrorMessage = "El color es requerido")]
    public required string Color { get; set; }

    [Required(ErrorMessage = "El material es requerido")]
    public required string Material { get; set; }

    [Required(ErrorMessage = "El precio de renta es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal PrecioDeRenta { get; set; }

    [Required(ErrorMessage = "La categoría es requerida")]
    public int CategoriaId { get; set; }

    [Url(ErrorMessage = "La URL de la imagen no es válida")]
    public string? ImagenUrl { get; set; }
}
