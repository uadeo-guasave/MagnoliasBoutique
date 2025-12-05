using System.ComponentModel.DataAnnotations;
using MagnoliaDB;

namespace MagnoliaWebAPI10.DTOs;

public class RentaDTO
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public DateTime FechaRenta { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    public decimal Total { get; set; }
    public string Estado { get; set; } = "Activa";
}

public class RentaCreateDTO
{
    [Required(ErrorMessage = "El cliente es requerido")]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "La fecha de renta es requerida")]
    public DateTime FechaRenta { get; set; }

    public DateTime? FechaDevolucion { get; set; }

    [Required(ErrorMessage = "El total es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0")]
    public decimal Total { get; set; }

    public EstadoRenta Estado { get; set; } = EstadoRenta.Activa;
}

public class RentaUpdateDTO
{
    [Required(ErrorMessage = "El cliente es requerido")]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "La fecha de renta es requerida")]
    public DateTime FechaRenta { get; set; }

    public DateTime? FechaDevolucion { get; set; }

    [Required(ErrorMessage = "El total es requerido")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0")]
    public decimal Total { get; set; }

    public EstadoRenta Estado { get; set; }
}
