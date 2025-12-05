using System.ComponentModel.DataAnnotations;

namespace MagnoliaWebAPI10.DTOs;

public class DetalleRentaDTO
{
    public int Id { get; set; }
    public int RentaId { get; set; }
    public int PrendaId { get; set; }
    public decimal PrecioRenta { get; set; }
}

public class DetalleRentaCreateDTO
{
    [Required(ErrorMessage = "La renta es requerida")]
    public int RentaId { get; set; }

    [Required(ErrorMessage = "La prenda es requerida")]
    public int PrendaId { get; set; }

    [Required(ErrorMessage = "El precio de renta es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal PrecioRenta { get; set; }
}

public class DetalleRentaUpdateDTO
{
    [Required(ErrorMessage = "La renta es requerida")]
    public int RentaId { get; set; }

    [Required(ErrorMessage = "La prenda es requerida")]
    public int PrendaId { get; set; }

    [Required(ErrorMessage = "El precio de renta es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal PrecioRenta { get; set; }
}
