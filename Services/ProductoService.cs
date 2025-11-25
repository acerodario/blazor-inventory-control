using Microsoft.EntityFrameworkCore;
using PruebaFinal.Data;
using PruebaFinal.Models;
//original
namespace PruebaFinal.Services
{
    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _context;

        public ProductoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CrearAsync(Producto producto)
        {
            try
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CodigoExisteAsync(string codigo)
        {
            return await _context.Productos.AnyAsync(p => p.Codigo == codigo);
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos
                .OrderBy(p => p.Nombre)
                .ToListAsync();
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<bool> ActualizarAsync(Producto producto)
        {
            try
            {
                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                    return false;

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Producto>> BuscarProductosAsync(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return new List<Producto>();

            var terminoLower = termino.ToLower();
            return await _context.Productos
                .Where(p => p.Nombre.ToLower().Contains(terminoLower) ||
                           p.Codigo.ToLower().Contains(terminoLower) ||
                           (p.Marca != null && p.Marca.ToLower().Contains(terminoLower)))
                .OrderBy(p => p.Nombre)
                .ToListAsync();
        }

        public async Task<bool> AjustarStockAsync(int idProducto, int nuevaCantidad)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(idProducto);
                if (producto == null)
                    return false;

                producto.Stock = nuevaCantidad;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}