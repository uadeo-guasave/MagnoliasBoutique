using System.ComponentModel.DataAnnotations;

namespace MagnoliaWebAPI10.DTOs;

public class ClienteDTO
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public required string Telefono { get; set; }
    public string? Email { get; set; }
}

public class ClienteCreateDTO
{
    [Required(ErrorMessage = "El nombre es requerido")]
    [MaxLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
    public required string Nombre { get; set; }

    [Required(ErrorMessage = "El teléfono es requerido")]
    [MaxLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
    [Phone(ErrorMessage = "El formato del teléfono no es válido")]
    public required string Telefono { get; set; }

    [MaxLength(200, ErrorMessage = "El email no puede exceder 200 caracteres")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    public string? Email { get; set; }
}

public class ClienteUpdateDTO
{
    [Required(ErrorMessage = "El nombre es requerido")]
    [MaxLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
    public required string Nombre { get; set; }

    [Required(ErrorMessage = "El teléfono es requerido")]
    [MaxLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
    [Phone(ErrorMessage = "El formato del teléfono no es válido")]
    public required string Telefono { get; set; }

    [MaxLength(200, ErrorMessage = "El email no puede exceder 200 caracteres")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    public string? Email { get; set; }
}
