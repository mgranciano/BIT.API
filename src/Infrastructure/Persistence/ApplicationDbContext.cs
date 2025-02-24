using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

/// <summary>
/// Representa el contexto de base de datos para la aplicación.
/// </summary>
/// <remarks>
/// Esta clase extiende `DbContext` de Entity Framework Core y gestiona el acceso a la base de datos,
/// incluyendo la configuración de las entidades y sus relaciones.
/// </remarks>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Constructor del contexto de base de datos.
    /// </summary>
    /// <param name="options">Opciones de configuración para `DbContext`.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    /// <summary>
    /// Conjunto de datos de usuarios en la base de datos.
    /// </summary>
    public DbSet<Usuario> Usuarios { get; set; }

    /// <summary>
    /// Configuración del modelo de base de datos y sus relaciones.
    /// </summary>
    /// <param name="modelBuilder">Constructor del modelo de base de datos.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /// <summary>
        /// Configura la clave primaria de la entidad `Usuario`.
        /// </summary>
        modelBuilder.Entity<Usuario>().HasKey(u => u.UsuarioId);
    }
}
