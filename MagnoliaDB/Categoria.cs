using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagnoliaDB;

[Table("Categorias")]
public class Categoria
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; }

    public bool EstaDisponible { get; set; } = true;
    
}
