using System.ComponentModel.DataAnnotations;

namespace MagnoliaWebAPI10.DTOs;

public class CategoriaDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [MaxLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
    public required string Nombre { get; set; }

    public bool EstaDisponible { get; set; } = true;
}

public class CategoriaCreateDTO
{
    [Required(ErrorMessage = "El nombre es requerido")]
    [MaxLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
    public required string Nombre { get; set; }

    public bool EstaDisponible { get; set; } = true;
}

public class CategoriaUpdateDTO
{
    [Required(ErrorMessage = "El nombre es requerido")]
    [MaxLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
    public required string Nombre { get; set; }

    public bool EstaDisponible { get; set; }
}
