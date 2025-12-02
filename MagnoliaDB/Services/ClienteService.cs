using Microsoft.EntityFrameworkCore;

namespace MagnoliaDB.Services;

public class ClienteService
{
    private readonly SqliteDbContext _context;

    public ClienteService(SqliteDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cliente>> ObtenerTodosAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<Cliente?> ObtenerPorIdAsync(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<List<Cliente>> BuscarPorNombreAsync(string nombre)
    {
        return await _context.Clientes
            .Where(c => c.Nombre.Contains(nombre))
            .ToListAsync();
    }

    public async Task<Cliente?> ObtenerPorTelefonoAsync(string telefono)
    {
        return await _context.Clientes
            .FirstOrDefaultAsync(c => c.Telefono == telefono);
    }

    public async Task<Cliente> CrearAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<bool> ActualizarAsync(Cliente cliente)
    {
        _context.Entry(cliente).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ExisteAsync(cliente.Id))
                return false;
            throw;
        }
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
            return false;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Clientes.AnyAsync(c => c.Id == id);
    }
}
