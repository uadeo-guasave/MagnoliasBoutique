using Microsoft.AspNetCore.Mvc;
using MagnoliaDB.Services;
using MagnoliaWebAPI10.DTOs;
using MagnoliaWebAPI10.Mappers;

namespace MagnoliaWebAPI10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetallesRentaController : ControllerBase
{
    private readonly DetalleRentaService _service;

    public DetallesRentaController(DetalleRentaService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todos los detalles de renta
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<DetalleRentaDTO>>> GetAll()
    {
        var detalles = await _service.ObtenerTodosAsync();
        return Ok(detalles.Select(d => d.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene un detalle de renta por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<DetalleRentaDTO>> GetById(int id)
    {
        var detalle = await _service.ObtenerPorIdAsync(id);
        if (detalle == null)
            return NotFound(new { message = $"Detalle de renta con ID {id} no encontrado" });

        return Ok(detalle.ToDTO());
    }

    /// <summary>
    /// Obtiene los detalles de una renta específica
    /// </summary>
    [HttpGet("renta/{rentaId}")]
    public async Task<ActionResult<List<DetalleRentaDTO>>> GetByRenta(int rentaId)
    {
        var detalles = await _service.ObtenerPorRentaAsync(rentaId);
        return Ok(detalles.Select(d => d.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene los detalles de renta de una prenda específica
    /// </summary>
    [HttpGet("prenda/{prendaId}")]
    public async Task<ActionResult<List<DetalleRentaDTO>>> GetByPrenda(int prendaId)
    {
        var detalles = await _service.ObtenerPorPrendaAsync(prendaId);
        return Ok(detalles.Select(d => d.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene el conteo de veces que se ha rentado una prenda
    /// </summary>
    [HttpGet("prenda/{prendaId}/conteo")]
    public async Task<ActionResult<int>> GetConteoRentas(int prendaId)
    {
        var conteo = await _service.ContarPrendasRentadasAsync(prendaId);
        return Ok(new { prendaId, vecesRentada = conteo });
    }

    /// <summary>
    /// Crea un nuevo detalle de renta
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<DetalleRentaDTO>> Create([FromBody] DetalleRentaCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var detalle = dto.ToEntity();
        var created = await _service.CrearAsync(detalle);
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToDTO());
    }

    /// <summary>
    /// Actualiza un detalle de renta existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DetalleRentaUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var detalle = await _service.ObtenerPorIdAsync(id);
        if (detalle == null)
            return NotFound(new { message = $"Detalle de renta con ID {id} no encontrado" });

        dto.UpdateEntity(detalle);
        var success = await _service.ActualizarAsync(detalle);

        if (!success)
            return StatusCode(500, new { message = "Error al actualizar el detalle de renta" });

        return NoContent();
    }

    /// <summary>
    /// Elimina un detalle de renta
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.EliminarAsync(id);
        if (!success)
            return NotFound(new { message = $"Detalle de renta con ID {id} no encontrado" });

        return NoContent();
    }
}
