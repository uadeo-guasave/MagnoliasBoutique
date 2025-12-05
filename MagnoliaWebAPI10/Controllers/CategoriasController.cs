using Microsoft.AspNetCore.Mvc;
using MagnoliaDB.Services;
using MagnoliaWebAPI10.DTOs;
using MagnoliaWebAPI10.Mappers;

namespace MagnoliaWebAPI10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly CategoriaService _service;

    public CategoriasController(CategoriaService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todas las categorías
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<CategoriaDTO>>> GetAll()
    {
        var categorias = await _service.ObtenerTodasAsync();
        return Ok(categorias.Select(c => c.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene las categorías disponibles
    /// </summary>
    [HttpGet("disponibles")]
    public async Task<ActionResult<List<CategoriaDTO>>> GetDisponibles()
    {
        var categorias = await _service.ObtenerDisponiblesAsync();
        return Ok(categorias.Select(c => c.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene una categoría por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaDTO>> GetById(int id)
    {
        var categoria = await _service.ObtenerPorIdAsync(id);
        if (categoria == null)
            return NotFound(new { message = $"Categoría con ID {id} no encontrada" });

        return Ok(categoria.ToDTO());
    }

    /// <summary>
    /// Crea una nueva categoría
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CategoriaDTO>> Create([FromBody] CategoriaCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var categoria = dto.ToEntity();
        var created = await _service.CrearAsync(categoria);
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToDTO());
    }

    /// <summary>
    /// Actualiza una categoría existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoriaUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var categoria = await _service.ObtenerPorIdAsync(id);
        if (categoria == null)
            return NotFound(new { message = $"Categoría con ID {id} no encontrada" });

        dto.UpdateEntity(categoria);
        var success = await _service.ActualizarAsync(categoria);

        if (!success)
            return StatusCode(500, new { message = "Error al actualizar la categoría" });

        return NoContent();
    }

    /// <summary>
    /// Elimina una categoría
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.EliminarAsync(id);
        if (!success)
            return NotFound(new { message = $"Categoría con ID {id} no encontrada" });

        return NoContent();
    }
}
