using Application.DTOs.Login;
using Dominio.Entities;

namespace Application.Extensions
{
    public static class DatosAccesoUsuarioExtensions
    {
        public static DatosAccesoUsuarioDto ToDatosAccesoUsuario(this AccesoUsuario usuario)
        {
            return new DatosAccesoUsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                NombreCompleto = usuario.NombreCompleto,
                CorreoElectronico = usuario.CorreoElectronico,
                Estado = usuario.Estado,
                Pais = usuario.Pais,
                Rol = usuario.Rol
            };
        }
    }
}