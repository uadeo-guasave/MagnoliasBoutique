using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagnoliaDB;

[Table("Rentas")]
public class Renta
{
    public int Id { get; set; }

    [Required]
    public int ClienteId { get; set; }

    [Required]
    public DateTime FechaRenta { get; set; }

    public DateTime? FechaDevolucion { get; set; }

    [Required]
    public decimal Total { get; set; }

    [Required]
    public EstadoRenta Estado { get; set; } = EstadoRenta.Activa;

    // EFCore - Navegación
    [NotMapped]
    public Cliente? Cliente { get; set; }

    [NotMapped]
    public List<DetalleRenta>? DetallesRenta { get; set; }
}
