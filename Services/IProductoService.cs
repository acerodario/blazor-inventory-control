using PruebaFinal.Models;

namespace PruebaFinal.Services
    //original
{
    public interface IProductoService
    {
        Task<bool> CrearAsync(Producto producto);
        Task<bool> CodigoExisteAsync(string codigo);

        // Métodos para editar
        Task<List<Producto>> ObtenerTodosAsync();
        Task<Producto?> ObtenerPorIdAsync(int id);
        Task<bool> ActualizarAsync(Producto producto);

        // Método para eliminar
        Task<bool> EliminarAsync(int id);

        // Métodos para ajustar
        Task<List<Producto>> BuscarProductosAsync(string termino);
        Task<bool> AjustarStockAsync(int idProducto, int nuevaCantidad);


    }
}