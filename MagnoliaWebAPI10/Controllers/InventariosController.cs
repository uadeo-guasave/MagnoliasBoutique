using Microsoft.AspNetCore.Mvc;
using MagnoliaDB.Services;
using MagnoliaWebAPI10.DTOs;
using MagnoliaWebAPI10.Mappers;

namespace MagnoliaWebAPI10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventariosController : ControllerBase
{
    private readonly InventarioService _service;

    public InventariosController(InventarioService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todos los registros de inventario
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<InventarioDTO>>> GetAll()
    {
        var inventarios = await _service.ObtenerTodosAsync();
        return Ok(inventarios.Select(i => i.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene un registro de inventario por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<InventarioDTO>> GetById(int id)
    {
        var inventario = await _service.ObtenerPorIdAsync(id);
        if (inventario == null)
            return NotFound(new { message = $"Inventario con ID {id} no encontrado" });

        return Ok(inventario.ToDTO());
    }

    /// <summary>
    /// Obtiene el historial de inventario de una prenda
    /// </summary>
    [HttpGet("prenda/{prendaId}")]
    public async Task<ActionResult<List<InventarioDTO>>> GetByPrenda(int prendaId)
    {
        var inventarios = await _service.ObtenerPorPrendaAsync(prendaId);
        return Ok(inventarios.Select(i => i.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene la existencia actual de una prenda
    /// </summary>
    [HttpGet("prenda/{prendaId}/existencia")]
    public async Task<ActionResult<int>> GetExistencia(int prendaId)
    {
        var existencia = await _service.ObtenerExistenciaTotalAsync(prendaId);
        return Ok(new { prendaId, existencia });
    }

    /// <summary>
    /// Crea un nuevo registro de inventario
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<InventarioDTO>> Create([FromBody] InventarioCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var inventario = dto.ToEntity();
        var created = await _service.CrearAsync(inventario);
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToDTO());
    }

    /// <summary>
    /// Actualiza un registro de inventario existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] InventarioUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var inventario = await _service.ObtenerPorIdAsync(id);
        if (inventario == null)
            return NotFound(new { message = $"Inventario con ID {id} no encontrado" });

        dto.UpdateEntity(inventario);
        var success = await _service.ActualizarAsync(inventario);

        if (!success)
            return StatusCode(500, new { message = "Error al actualizar el inventario" });

        return NoContent();
    }

    /// <summary>
    /// Elimina un registro de inventario
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.EliminarAsync(id);
        if (!success)
            return NotFound(new { message = $"Inventario con ID {id} no encontrado" });

        return NoContent();
    }
}
