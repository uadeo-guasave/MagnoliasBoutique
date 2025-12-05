using Microsoft.AspNetCore.Mvc;
using MagnoliaDB;
using MagnoliaDB.Services;
using MagnoliaWebAPI10.DTOs;
using MagnoliaWebAPI10.Mappers;

namespace MagnoliaWebAPI10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentasController : ControllerBase
{
    private readonly RentaService _service;

    public RentasController(RentaService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todas las rentas
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<RentaDTO>>> GetAll()
    {
        var rentas = await _service.ObtenerTodasAsync();
        return Ok(rentas.Select(r => r.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene una renta por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<RentaDTO>> GetById(int id)
    {
        var renta = await _service.ObtenerPorIdAsync(id);
        if (renta == null)
            return NotFound(new { message = $"Renta con ID {id} no encontrada" });

        return Ok(renta.ToDTO());
    }

    /// <summary>
    /// Obtiene las rentas de un cliente
    /// </summary>
    [HttpGet("cliente/{clienteId}")]
    public async Task<ActionResult<List<RentaDTO>>> GetByCliente(int clienteId)
    {
        var rentas = await _service.ObtenerPorClienteAsync(clienteId);
        return Ok(rentas.Select(r => r.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene las rentas activas
    /// </summary>
    [HttpGet("activas")]
    public async Task<ActionResult<List<RentaDTO>>> GetActivas()
    {
        var rentas = await _service.ObtenerActivasAsync();
        return Ok(rentas.Select(r => r.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene las rentas retrasadas
    /// </summary>
    [HttpGet("retrasadas")]
    public async Task<ActionResult<List<RentaDTO>>> GetRetrasadas()
    {
        var rentas = await _service.ObtenerRetrasadasAsync();
        return Ok(rentas.Select(r => r.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene rentas por estado
    /// </summary>
    [HttpGet("estado/{estado}")]
    public async Task<ActionResult<List<RentaDTO>>> GetByEstado(EstadoRenta estado)
    {
        var rentas = await _service.ObtenerPorEstadoAsync(estado);
        return Ok(rentas.Select(r => r.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene rentas por rango de fechas
    /// </summary>
    [HttpGet("fechas")]
    public async Task<ActionResult<List<RentaDTO>>> GetByFechas(
        [FromQuery] DateTime fechaInicio, 
        [FromQuery] DateTime fechaFin)
    {
        var rentas = await _service.ObtenerPorRangoFechasAsync(fechaInicio, fechaFin);
        return Ok(rentas.Select(r => r.ToDTO()).ToList());
    }

    /// <summary>
    /// Crea una nueva renta
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<RentaDTO>> Create([FromBody] RentaCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var renta = dto.ToEntity();
        var created = await _service.CrearAsync(renta);
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToDTO());
    }

    /// <summary>
    /// Actualiza una renta existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RentaUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var renta = await _service.ObtenerPorIdAsync(id);
        if (renta == null)
            return NotFound(new { message = $"Renta con ID {id} no encontrada" });

        dto.UpdateEntity(renta);
        var success = await _service.ActualizarAsync(renta);

        if (!success)
            return StatusCode(500, new { message = "Error al actualizar la renta" });

        return NoContent();
    }

    /// <summary>
    /// Marca una renta como devuelta
    /// </summary>
    [HttpPatch("{id}/devolver")]
    public async Task<IActionResult> MarcarDevuelta(int id, [FromBody] DateTime? fechaDevolucion = null)
    {
        var fecha = fechaDevolucion ?? DateTime.Now;
        var success = await _service.MarcarComoDevueltaAsync(id, fecha);
        
        if (!success)
            return NotFound(new { message = $"Renta con ID {id} no encontrada" });

        return NoContent();
    }

    /// <summary>
    /// Marca una renta como retrasada
    /// </summary>
    [HttpPatch("{id}/retrasada")]
    public async Task<IActionResult> MarcarRetrasada(int id)
    {
        var success = await _service.MarcarComoRetrasadaAsync(id);
        
        if (!success)
            return NotFound(new { message = $"Renta con ID {id} no encontrada" });

        return NoContent();
    }

    /// <summary>
    /// Elimina una renta
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.EliminarAsync(id);
        if (!success)
            return NotFound(new { message = $"Renta con ID {id} no encontrada" });

        return NoContent();
    }
}
