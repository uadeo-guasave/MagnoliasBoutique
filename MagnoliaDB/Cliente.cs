using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagnoliaDB;

[Table("Clientes")]
public class Cliente
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public required string Nombre { get; set; }

    [Required, MaxLength(20)]
    public required string Telefono { get; set; }

    [MaxLength(200)]
    public string? Email { get; set; }

    // EFCore - Navegación
    [NotMapped]
    public List<Renta>? Rentas { get; set; }
}
