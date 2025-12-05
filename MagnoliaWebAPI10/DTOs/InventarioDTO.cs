using System.ComponentModel.DataAnnotations;

namespace MagnoliaWebAPI10.DTOs;

public class InventarioDTO
{
    public int Id { get; set; }
    public int PrendaId { get; set; }
    public decimal Costo { get; set; }
    public DateOnly FechaDeAlta { get; set; }
    public int Existencia { get; set; }
}

public class InventarioCreateDTO
{
    [Required(ErrorMessage = "La prenda es requerida")]
    public int PrendaId { get; set; }

    [Required(ErrorMessage = "El costo es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor a 0")]
    public decimal Costo { get; set; }

    [Required(ErrorMessage = "La fecha de alta es requerida")]
    public DateOnly FechaDeAlta { get; set; }

    [Required(ErrorMessage = "La existencia es requerida")]
    [Range(0, int.MaxValue, ErrorMessage = "La existencia no puede ser negativa")]
    public int Existencia { get; set; }
}

public class InventarioUpdateDTO
{
    [Required(ErrorMessage = "La prenda es requerida")]
    public int PrendaId { get; set; }

    [Required(ErrorMessage = "El costo es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El costo debe ser mayor a 0")]
    public decimal Costo { get; set; }

    [Required(ErrorMessage = "La fecha de alta es requerida")]
    public DateOnly FechaDeAlta { get; set; }

    [Required(ErrorMessage = "La existencia es requerida")]
    [Range(0, int.MaxValue, ErrorMessage = "La existencia no puede ser negativa")]
    public int Existencia { get; set; }
}
