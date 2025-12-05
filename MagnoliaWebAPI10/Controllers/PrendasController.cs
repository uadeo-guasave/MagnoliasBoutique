using Microsoft.AspNetCore.Mvc;
using MagnoliaDB.Services;
using MagnoliaWebAPI10.DTOs;
using MagnoliaWebAPI10.Mappers;

namespace MagnoliaWebAPI10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrendasController : ControllerBase
{
    private readonly PrendaService _service;

    public PrendasController(PrendaService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todas las prendas
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PrendaDTO>>> GetAll()
    {
        var prendas = await _service.ObtenerTodasAsync();
        return Ok(prendas.Select(p => p.ToDTO()).ToList());
    }

    /// <summary>
    /// Obtiene las prendas más recientes
    /// </summary>
    [HttpGet("recientes")]
    public async Task<ActionResult<List<MagnoliaDB.PrendaDTO>>> GetRecientes([FromQuery] int cantidad = 10)
    {
        var prendas = await _service.ObtenerMasRecientesAsync(cantidad);
        return Ok(prendas);
    }

    /// <summary>
    /// Obtiene una prenda por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PrendaDTO>> GetById(int id)
    {
        var prenda = await _service.ObtenerPorIdAsync(id);
        if (prenda == null)
            return NotFound(new { message = $"Prenda con ID {id} no encontrada" });

        return Ok(prenda.ToDTO());
    }

    /// <summary>
    /// Obtiene una prenda por código de referencia
    /// </summary>
    [HttpGet("codigo/{codigo}")]
    public async Task<ActionResult<PrendaDTO>> GetByCodigo(string codigo)
    {
        var prenda = await _service.ObtenerPorCodigoReferenciaAsync(codigo);
        if (prenda == null)
            return NotFound(new { message = $"Prenda con código {codigo} no encontrada" });

        return Ok(prenda.ToDTO());
    }

    /// <summary>
    /// Obtiene prendas por categoría
    /// </summary>
    [HttpGet("categoria/{categoriaId}")]
    public async Task<ActionResult<List<PrendaDTO>>> GetByCategoria(int categoriaId)
    {
        var prendas = await _service.ObtenerPorCategoriaAsync(categoriaId);
        return Ok(prendas.Select(p => p.ToDTO()).ToList());
    }

    /// <summary>
    /// Crea una nueva prenda
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PrendaDTO>> Create([FromBody] PrendaCreateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Verificar si el código ya existe
        if (await _service.CodigoReferenciaExisteAsync(dto.CodigoDeReferencia))
            return BadRequest(new { message = $"Ya existe una prenda con el código {dto.CodigoDeReferencia}" });

        var prenda = dto.ToEntity();
        var created = await _service.CrearAsync(prenda);
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToDTO());
    }

    /// <summary>
    /// Actualiza una prenda existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PrendaUpdateDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var prenda = await _service.ObtenerPorIdAsync(id);
        if (prenda == null)
            return NotFound(new { message = $"Prenda con ID {id} no encontrada" });

        // Verificar si el código ya existe en otra prenda
        if (await _service.CodigoReferenciaExisteAsync(dto.CodigoDeReferencia, id))
            return BadRequest(new { message = $"Ya existe otra prenda con el código {dto.CodigoDeReferencia}" });

        dto.UpdateEntity(prenda);
        var success = await _service.ActualizarAsync(prenda);

        if (!success)
            return StatusCode(500, new { message = "Error al actualizar la prenda" });

        return NoContent();
    }

    /// <summary>
    /// Elimina una prenda
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.EliminarAsync(id);
        if (!success)
            return NotFound(new { message = $"Prenda con ID {id} no encontrada" });

        return NoContent();
    }
}
