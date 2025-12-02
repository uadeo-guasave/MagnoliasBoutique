using Microsoft.EntityFrameworkCore;

namespace MagnoliaDB.Services;

public class InventarioService
{
    private readonly SqliteDbContext _context;

    public InventarioService(SqliteDbContext context)
    {
        _context = context;
    }

    public async Task<List<Inventario>> ObtenerTodosAsync()
    {
        return await _context.Inventarios.ToListAsync();
    }

    public async Task<Inventario?> ObtenerPorIdAsync(int id)
    {
        return await _context.Inventarios.FindAsync(id);
    }

    public async Task<List<Inventario>> ObtenerPorPrendaAsync(int prendaId)
    {
        return await _context.Inventarios
            .Where(i => i.PrendaId == prendaId)
            .OrderByDescending(i => i.FechaDeAlta)
            .ToListAsync();
    }

    public async Task<Inventario?> ObtenerUltimoPorPrendaAsync(int prendaId)
    {
        return await _context.Inventarios
            .Where(i => i.PrendaId == prendaId)
            .OrderByDescending(i => i.FechaDeAlta)
            .FirstOrDefaultAsync();
    }

    public async Task<int> ObtenerExistenciaTotalAsync(int prendaId)
    {
        var ultimo = await ObtenerUltimoPorPrendaAsync(prendaId);
        return ultimo?.Existencia ?? 0;
    }

    public async Task<Inventario> CrearAsync(Inventario inventario)
    {
        _context.Inventarios.Add(inventario);
        await _context.SaveChangesAsync();
        return inventario;
    }

    public async Task<bool> ActualizarAsync(Inventario inventario)
    {
        _context.Entry(inventario).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ExisteAsync(inventario.Id))
                return false;
            throw;
        }
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var inventario = await _context.Inventarios.FindAsync(id);
        if (inventario == null)
            return false;

        _context.Inventarios.Remove(inventario);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Inventarios.AnyAsync(i => i.Id == id);
    }
}
