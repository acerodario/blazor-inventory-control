using Microsoft.EntityFrameworkCore;
using PruebaFinal.Data;
using PruebaFinal.Models;

namespace PruebaFinal.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ValidarUsuarioAsync(string nombreUsuario, string password)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario &&
                                         u.Password == password &&
                                         u.Activo);

            return usuario;
        }

        public Task<bool> CerrarSesionAsync()
        {
            return Task.FromResult(true);
        }
    }
}