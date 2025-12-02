using Microsoft.EntityFrameworkCore;

namespace MagnoliaDB.Services;

public class CategoriaService
{
    private readonly SqliteDbContext _context;

    public CategoriaService(SqliteDbContext context)
    {
        _context = context;
    }

    public async Task<List<Categoria>> ObtenerTodasAsync()
    {
        return await _context.Categorias.ToListAsync();
    }

    public async Task<List<Categoria>> ObtenerDisponiblesAsync()
    {
        return await _context.Categorias
            .Where(c => c.EstaDisponible)
            .ToListAsync();
    }

    public async Task<Categoria?> ObtenerPorIdAsync(int id)
    {
        return await _context.Categorias.FindAsync(id);
    }

    public async Task<Categoria> CrearAsync(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<bool> ActualizarAsync(Categoria categoria)
    {
        _context.Entry(categoria).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ExisteAsync(categoria.Id))
                return false;
            throw;
        }
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
            return false;

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Categorias.AnyAsync(c => c.Id == id);
    }
}
