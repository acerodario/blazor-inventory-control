using PruebaFinal.Models;

namespace PruebaFinal.Services
{
    public interface IAuthService
    {
        Task<Usuario?> ValidarUsuarioAsync(string nombreUsuario, string password);
        Task<bool> CerrarSesionAsync();
    }
}