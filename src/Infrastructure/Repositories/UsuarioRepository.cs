using Dapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio para gestionar las consultas de la entidad Usuario.
    /// Implementa la obtención de usuarios mediante Dapper y SQL Server.
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        private readonly string sp_ValidarUsuario = "[dbo].[sp_ValidarUsuario]";
        private readonly string sp_ObtenerUsuarios = "[dbo].[sp_UsuarioCompleto]";
        private readonly string sp_ObtenerModulosPorUsuario = "[dbo].[sp_ObtenerMenusSubMenus]";



        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Task<Usuario?> ActualizarUsuarioAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario?> EliminarUsuarioAsync(string usuarioId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene los modulos a los que tiene acceso un usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns>Retona una lista de modulos si el usuario no tiene asignados, se rotorna nulo</returns>

        public async Task<IEnumerable<ModuloGeneral>> ObtenerModulosPorUsuarioAsync(string idUsuario)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var resp = await connection.QueryAsync<ModuloGeneral>(
                        sp_ObtenerModulosPorUsuario,
                        new { IdUsuario = idUsuario },
                        commandType: CommandType.StoredProcedure
                    );
                    return resp;
                }
            }
            catch (SqlException ex)
            {

                throw new Exception("Se produjo un error al realizar la petición a nivel BD." + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al realizar la petición." + ex.Message);

            }
        }

        /// <summary>
        /// Obtiene un usuario por su correo electrónico ejecutando un Stored Procedure.
        /// </summary>
        /// <param name="correo">Correo del usuario a buscar.</param>
        /// <returns>Objeto Usuario si existe, null en caso contrario.</returns>
        public async Task<DatosAccesoUsuario?> ObtenerUsuarioPorEmailAsync(string correo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var resp = await connection.QueryFirstOrDefaultAsync<DatosAccesoUsuario>(
                    sp_ValidarUsuario,
                    new { CorreoElectronico = correo },
                    commandType: CommandType.StoredProcedure
                );
                return resp;
            }

        }


        public Task<Usuario?> ObtenerUsuarioPorIdAsync(string usuarioId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene la lista completa de usuarios registrados en el sistema, así también como los roles asignado y los paises.
        /// </summary>
        /// <returns>Retorna la lista de usuario que están registrados en el sistema</returns>
        public async Task<IEnumerable<Usuario>> ObtenerUsuariosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var resp = await connection.QueryAsync<Usuario>(
                    sp_ObtenerUsuarios,
                    commandType: CommandType.StoredProcedure
                );
                return resp;
            }
        }

        public Task<Usuario> RegistrarUsuarioAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
