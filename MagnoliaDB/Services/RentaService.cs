using Microsoft.EntityFrameworkCore;

namespace MagnoliaDB.Services;

public class RentaService
{
    private readonly SqliteDbContext _context;

    public RentaService(SqliteDbContext context)
    {
        _context = context;
    }

    public async Task<List<Renta>> ObtenerTodasAsync()
    {
        return await _context.Rentas.ToListAsync();
    }

    public async Task<Renta?> ObtenerPorIdAsync(int id)
    {
        return await _context.Rentas.FindAsync(id);
    }

    public async Task<List<Renta>> ObtenerPorClienteAsync(int clienteId)
    {
        return await _context.Rentas
            .Where(r => r.ClienteId == clienteId)
            .OrderByDescending(r => r.FechaRenta)
            .ToListAsync();
    }

    public async Task<List<Renta>> ObtenerActivasAsync()
    {
        return await _context.Rentas
            .Where(r => r.Estado == EstadoRenta.Activa)
            .OrderByDescending(r => r.FechaRenta)
            .ToListAsync();
    }

    public async Task<List<Renta>> ObtenerRetrasadasAsync()
    {
        return await _context.Rentas
            .Where(r => r.Estado == EstadoRenta.Retrasada)
            .OrderBy(r => r.FechaRenta)
            .ToListAsync();
    }

    public async Task<List<Renta>> ObtenerPorEstadoAsync(EstadoRenta estado)
    {
        return await _context.Rentas
            .Where(r => r.Estado == estado)
            .OrderByDescending(r => r.FechaRenta)
            .ToListAsync();
    }

    public async Task<List<Renta>> ObtenerPorRangoFechasAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        return await _context.Rentas
            .Where(r => r.FechaRenta >= fechaInicio && r.FechaRenta <= fechaFin)
            .OrderByDescending(r => r.FechaRenta)
            .ToListAsync();
    }

    public async Task<Renta> CrearAsync(Renta renta)
    {
        _context.Rentas.Add(renta);
        await _context.SaveChangesAsync();
        return renta;
    }

    public async Task<bool> ActualizarAsync(Renta renta)
    {
        _context.Entry(renta).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ExisteAsync(renta.Id))
                return false;
            throw;
        }
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var renta = await _context.Rentas.FindAsync(id);
        if (renta == null)
            return false;

        _context.Rentas.Remove(renta);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Rentas.AnyAsync(r => r.Id == id);
    }

    public async Task<bool> MarcarComoDevueltaAsync(int id, DateTime fechaDevolucion)
    {
        var renta = await ObtenerPorIdAsync(id);
        if (renta == null)
            return false;

        renta.Estado = EstadoRenta.Devuelta;
        renta.FechaDevolucion = fechaDevolucion;
        
        return await ActualizarAsync(renta);
    }

    public async Task<bool> MarcarComoRetrasadaAsync(int id)
    {
        var renta = await ObtenerPorIdAsync(id);
        if (renta == null)
            return false;

        renta.Estado = EstadoRenta.Retrasada;
        
        return await ActualizarAsync(renta);
    }
}
