namespace Application.Extensions;

using Application.DTOs;
using Dominio.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Métodos de extensión para convertir entidades de Usuario a DTOs.
/// </summary>
public static class UsuarioExtensions
{
    public static UsuarioDto ToDto(this Usuario usuario)
    {
        return new UsuarioDto
        {
            UsuarioId = usuario.IdUsuario,
            Email = usuario.CorreoElectronico,
            Nombre = usuario.NombreCompleto,
            UsuarioSuperiorId = usuario.IdSuperior,
            NombreSuperior = usuario.NombreSuperior,
            EmailSuperior = usuario.CorreoElectronicoSuperior,
            Paises = usuario.Paises?.ToPaisDtoList(),
            Roles = usuario.Roles?.ToRolDtoList(),
            FechaCreacion = usuario.FechaCreacion,
            FechaActualizacion = usuario.FechaActualizacion,
            Estatus = usuario.Estado
        };
    }

    public static Usuario ToEntity(this UsuarioDto usuarioDto)
    {
        return new Usuario
        {
            IdUsuario = usuarioDto.UsuarioId,
            CorreoElectronico = usuarioDto.Email,
            NombreCompleto = usuarioDto.Nombre,
            IdSuperior = usuarioDto.UsuarioSuperiorId,
            NombreSuperior = usuarioDto.NombreSuperior,
            CorreoElectronicoSuperior = usuarioDto.EmailSuperior,
            Paises = usuarioDto.Paises?.ToPaisEntityList(),
            Roles = usuarioDto.Roles?.ToRolEntityList(),
            FechaCreacion = usuarioDto.FechaCreacion,
            FechaActualizacion = usuarioDto.FechaActualizacion,
            Estado = usuarioDto.Estatus
        };
    }

    public static List<Pais> ToPaisEntityList(this IEnumerable<PaisDto> paises)
    {
        return paises?.Select(p => new Pais
        {
            Codigo = p.PaisId,
            Estado = p.Estado
        }).ToList() ?? new List<Pais>();
    }

    public static List<Rol> ToRolEntityList(this IEnumerable<RolDto> roles)
    {
        return roles?.Select(r => new Rol
        {
            Id = r.RolId,
            Estado = r.Estado
        }).ToList() ?? new List<Rol>();
    }

    public static IEnumerable<UsuarioDto> ToDtoList(this IEnumerable<Usuario> usuarios)
    {
        return usuarios.Select(u => u.ToDto());
    }

    public static List<PaisDto> ToPaisDtoList(this List<Pais> paises)
    {
        return paises.Select(p => new PaisDto
        {
            PaisId = p.Codigo,
            Nombre = p.Nombre,
            Estado = p.Estado,

        }).ToList();
    }

    public static List<RolDto> ToRolDtoList(this List<Rol> roles)
    {
        return roles.Select(p => new RolDto
        {
            RolId = p.Id,
            Nombre = p.Nombre,
            Estado = p.Estado,

        }).ToList();
    }
}
