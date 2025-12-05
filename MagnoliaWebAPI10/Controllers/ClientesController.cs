using Microsoft.AspNetCore.Mvc;
using MagnoliaDB.Services;
using MagnoliaWebAPI10.DTOs;
using MagnoliaWebAPI10.Mappers;

namespace MagnoliaWebAPI10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly ClienteService _service;

    public ClientesController(ClienteService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todos los clientes
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<ClienteDTO>>> GetAll()
    {
        var clientes = await _service.ObtenerTodosAsync();
        return Ok(clientes.Select(c => c.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene un cliente por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteDTO>> GetById(int id)
    {
        var cliente = await _service.ObtenerPorIdAsync(id);
        if (cliente == null)
            return NotFound(new { message = $"Cliente con ID {id} no encontrado" });

        return Ok(cliente.ToDTO());
    }

    /// <summary>
    /// Busca clientes por nombre
    /// </summary>
    [HttpGet("buscar")]
    public async Task<ActionResult<List<ClienteDTO>>> BuscarPorNombre([FromQuery] string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return BadRequest(new { message = "El nombre de búsqueda es requerido" });

        var clientes = await _service.BuscarPorNombreAsync(nombre);
        return Ok(clientes.Select(c => c.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene un cliente por teléfono
    /// </summary>
    [HttpGet("telefono/{telefono}")]
    public async Task<ActionResult<ClienteDTO>> GetByTelefono(string telefono)
    {
        var cliente = await _service.ObtenerPorTelefonoAsync(telefono);
        if (cliente == null)
            return NotFound(new { message = $"Cliente con teléfono {telefono} no encontrado" });

        return Ok(cliente.ToDTO());
    }

    /// <summary>
    /// Crea un nuevo cliente
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ClienteDTO>> Create([FromBody] ClienteCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cliente = dto.ToEntity();
        var created = await _service.CrearAsync(cliente);
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToDTO());
    }

    /// <summary>
    /// Actualiza un cliente existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ClienteUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cliente = await _service.ObtenerPorIdAsync(id);
        if (cliente == null)
            return NotFound(new { message = $"Cliente con ID {id} no encontrado" });

        dto.UpdateEntity(cliente);
        var success = await _service.ActualizarAsync(cliente);

        if (!success)
            return StatusCode(500, new { message = "Error al actualizar el cliente" });

        return NoContent();
    }

    /// <summary>
    /// Elimina un cliente
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.EliminarAsync(id);
        if (!success)
            return NotFound(new { message = $"Cliente con ID {id} no encontrado" });

        return NoContent();
    }
}
