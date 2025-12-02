using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagnoliaDB;

[Table("DetallesRenta")]
public class DetalleRenta
{
    public int Id { get; set; }

    [Required]
    public int RentaId { get; set; }

    [Required]
    public int PrendaId { get; set; }

    [Required]
    public decimal PrecioRenta { get; set; }

    // EFCore - Navegación
    [NotMapped]
    public Renta? Renta { get; set; }

    [NotMapped]
    public Prenda? Prenda { get; set; }
}
