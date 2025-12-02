using Microsoft.EntityFrameworkCore;

namespace MagnoliaDB.Services;

public class PrendaService
{
    private readonly SqliteDbContext _context;

    public PrendaService(SqliteDbContext context)
    {
        _context = context;
    }

    public async Task<List<Prenda>> ObtenerTodasAsync()
    {
        return await _context.Prendas.ToListAsync();
    }

    public async Task<Prenda?> ObtenerPorIdAsync(int id)
    {
        return await _context.Prendas.FindAsync(id);
    }

    public async Task<Prenda?> ObtenerPorCodigoReferenciaAsync(string codigo)
    {
        return await _context.Prendas
            .FirstOrDefaultAsync(p => p.CodigoDeReferencia == codigo);
    }

    public async Task<List<Prenda>> ObtenerPorCategoriaAsync(int categoriaId)
    {
        return await _context.Prendas
            .Where(p => p.CategoriaId == categoriaId)
            .ToListAsync();
    }

    public async Task<List<PrendaDTO>> ObtenerMasRecientesAsync(int cantidad)
    {
        return await _context.Prendas
            .OrderByDescending(p => p.Id)
            .Take(cantidad)
            .Select(p => new PrendaDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                ImagenUrl = p.ImagenUrl
            })
            .ToListAsync();
    }

    public async Task<Prenda> CrearAsync(Prenda prenda)
    {
        _context.Prendas.Add(prenda);
        await _context.SaveChangesAsync();
        return prenda;
    }

    public async Task<bool> ActualizarAsync(Prenda prenda)
    {
        _context.Entry(prenda).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ExisteAsync(prenda.Id))
                return false;
            throw;
        }
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var prenda = await _context.Prendas.FindAsync(id);
        if (prenda == null)
            return false;

        _context.Prendas.Remove(prenda);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Prendas.AnyAsync(p => p.Id == id);
    }

    public async Task<bool> CodigoReferenciaExisteAsync(string codigo, int? excludeId = null)
    {
        return await _context.Prendas
            .AnyAsync(p => p.CodigoDeReferencia == codigo && p.Id != excludeId);
    }
}
