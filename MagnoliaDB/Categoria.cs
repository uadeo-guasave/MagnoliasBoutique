using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagnoliaDB;

[Table("Categorias")]
public class Categoria
{
    public int Id { get; set; }

    [Required]
    public required string Nombre { get; set; }

    public bool EstaDisponible { get; set; } = true;

    // EFCore
    [NotMapped]
    public List<Prenda>? Prendas { get; set; }
    
}
