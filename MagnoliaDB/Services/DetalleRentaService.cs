using Microsoft.EntityFrameworkCore;

namespace MagnoliaDB.Services;

public class DetalleRentaService
{
    private readonly SqliteDbContext _context;

    public DetalleRentaService(SqliteDbContext context)
    {
        _context = context;
    }

    public async Task<List<DetalleRenta>> ObtenerTodosAsync()
    {
        return await _context.DetallesRenta.ToListAsync();
    }

    public async Task<DetalleRenta?> ObtenerPorIdAsync(int id)
    {
        return await _context.DetallesRenta.FindAsync(id);
    }

    public async Task<List<DetalleRenta>> ObtenerPorRentaAsync(int rentaId)
    {
        return await _context.DetallesRenta
            .Where(dr => dr.RentaId == rentaId)
            .ToListAsync();
    }

    public async Task<List<DetalleRenta>> ObtenerPorPrendaAsync(int prendaId)
    {
        return await _context.DetallesRenta
            .Where(dr => dr.PrendaId == prendaId)
            .ToListAsync();
    }

    public async Task<int> ContarPrendasRentadasAsync(int prendaId)
    {
        return await _context.DetallesRenta
            .CountAsync(dr => dr.PrendaId == prendaId);
    }

    public async Task<DetalleRenta> CrearAsync(DetalleRenta detalleRenta)
    {
        _context.DetallesRenta.Add(detalleRenta);
        await _context.SaveChangesAsync();
        return detalleRenta;
    }

    public async Task<bool> ActualizarAsync(DetalleRenta detalleRenta)
    {
        _context.Entry(detalleRenta).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ExisteAsync(detalleRenta.Id))
                return false;
            throw;
        }
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var detalleRenta = await _context.DetallesRenta.FindAsync(id);
        if (detalleRenta == null)
            return false;

        _context.DetallesRenta.Remove(detalleRenta);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.DetallesRenta.AnyAsync(dr => dr.Id == id);
    }
}
