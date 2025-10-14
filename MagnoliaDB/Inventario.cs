using System.ComponentModel.DataAnnotations;

namespace MagnoliaDB;

public class Inventario
{
    public int Id { get; set; }

    [Required]
    public int PrendaId { get; set; }
    
    [Required]
    public decimal Costo { get; set; }

    [Required]
    public DateOnly FechaDeAlta { get; set; }

    [Required]
    public int Existencia { get; set; }
}
