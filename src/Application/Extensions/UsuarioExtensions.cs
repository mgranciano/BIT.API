namespace Application.Extensions;

using Application.DTOs;
using Dominio.Entities;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Métodos de extensión para convertir entidades de Usuario a DTOs.
/// </summary>
public static class UsuarioExtensions
{
    public static UsuarioDto ToDto(this Usuario usuario)
    {
        return new UsuarioDto
        {
            UsuarioId = usuario.UsuarioId,
            Email = usuario.Email,
            Nombre = usuario.Nombre,
            UsuarioSuperiorId = usuario.UsuarioSuperiorId,
            NombreSuperior = usuario.NombreSuperior,
            EmailSuperior = usuario.EmailSuperior,
            Paises = usuario.Paises?.ToPaisDtoList(),
            Roles = usuario.Roles?.ToRolDtoList(),
            FechaCreacion = usuario.FechaCreacion,
            FechaActualizacion = usuario.FechaActualizacion,
            Estatus = usuario.Estatus
        };
    }

    public static Usuario ToEntity(this UsuarioDto usuarioDto)
    {
        return new Usuario
        {
            UsuarioId = usuarioDto.UsuarioId,
            Email = usuarioDto.Email,
            Nombre = usuarioDto.Nombre,
            UsuarioSuperiorId = usuarioDto.UsuarioSuperiorId,
            NombreSuperior = usuarioDto.NombreSuperior,
            EmailSuperior = usuarioDto.EmailSuperior,
            Paises = usuarioDto.Paises?.ToPaisEntityList(),
            Roles = usuarioDto.Roles?.ToRolEntityList(),
            FechaCreacion = usuarioDto.FechaCreacion,
            FechaActualizacion = usuarioDto.FechaActualizacion,
            Estatus = usuarioDto.Estatus
        };
    }

    public static List<Pais> ToPaisEntityList(this IEnumerable<PaisDto> paises)
    {
        return paises?.Select(p => new Pais
        {
            PaisId = p.PaisId,
            Estado = p.Estado
        }).ToList() ?? new List<Pais>();
    }

    public static List<Rol> ToRolEntityList(this IEnumerable<RolDto> roles)
    {
        return roles?.Select(r => new Rol
        {
            RolId = r.RolId,
            Estado = r.Estado
        }).ToList() ?? new List<Rol>();
    }

    public static IEnumerable<UsuarioDto> ToDtoList(this IEnumerable<Usuario> usuarios)
    {
        return usuarios.Select(u => u.ToDto());
    }

    public static List<PaisDto> ToPaisDtoList(this IEnumerable<Pais> paises)
    {
        return paises.Select(p => new PaisDto
        {
            PaisId = p.PaisId,
            Estado = p.Estado
        }).ToList();
    }

    public static List<RolDto> ToRolDtoList(this IEnumerable<Rol> roles)
    {
        return roles.Select(r => new RolDto
        {
            RolId = r.RolId,
            Estado = r.Estado
        }).ToList();
    }
}
