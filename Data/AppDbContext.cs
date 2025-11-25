using Microsoft.EntityFrameworkCore;
using PruebaFinal.Models;

namespace PruebaFinal.Data
{
    /// <summary>
    /// Contexto de base de datos unificado para Productos y Usuarios
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets - Tablas de la base de datos
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========== CONFIGURACIÓN DE PRODUCTOS ==========
            modelBuilder.Entity<Producto>()
                .Property(p => p.PrecioCosto)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Producto>()
                .Property(p => p.PrecioVenta)
                .HasPrecision(18, 2);

            // ========== DATOS INICIALES PARA USUARIOS (SEED DATA) ==========
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    NombreUsuario = "admin",
                    Password = "admin123", // ⚠️ En producción usar hash (BCrypt, etc.)
                    Rol = "Administrador",
                    FechaCreacion = new DateTime(2024, 1, 1), // Fecha fija
                    Activo = true
                },
                new Usuario
                {
                    Id = 2,
                    NombreUsuario = "operador",
                    Password = "operador123", // ⚠️ En producción usar hash
                    Rol = "Operador",
                    FechaCreacion = new DateTime(2024, 1, 1), // Fecha fija
                    Activo = true
                }
            );
        }
    }
}