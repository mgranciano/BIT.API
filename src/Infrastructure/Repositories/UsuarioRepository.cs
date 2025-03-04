using Dapper;
using Dominio.Entities;
using Dominio.Entities.Dapper;
using Dominio.Interfaces;
using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio para gestionar las consultas de la entidad Usuario.
    /// Implementa la obtención de usuarios mediante Dapper y SQL Server.
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DapperContext _context;

        private readonly string sp_ValidarUsuario = "[dbo].[sp_ValidarUsuario]";
        private readonly string sp_ObtenerUsuarios = "[dbo].[sp_UsuarioCompleto]";
        private readonly string sp_ObtenerModulosPorUsuario = "[dbo].[sp_ObtenerMenusSubMenus]";
        private readonly string sp_RegistrarUsuario = "[dbo].[sp_InsertarUsuario]";
        private readonly string sp_ActualizarUsuario = "[dbo].[sp_ActualizarUsuario]";
        private readonly string sp_EliminarUsuario = "[dbo].[sp_ActualizarUsuario]";






        public UsuarioRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<RespuestaDapper> ActualizarUsuarioAsync(Usuario usuario)
        {
            try
            {
                RespuestaDapper resp;
                string responseJson = JsonSerializer.Serialize(usuario);
                using (var connection = _context.CreateConnection())
                {
                    var multi = await connection.QueryMultipleAsync(
                        sp_ActualizarUsuario,
                       new { Json = responseJson },
                        commandType: CommandType.StoredProcedure
                    );
                    resp = await multi.ReadFirstOrDefaultAsync<RespuestaDapper>() ?? new RespuestaDapper();
                }
                if (resp.Estatus != "E")
                {
                    return resp;
                }
                else
                {
                    throw new InvalidOperationException(resp?.Mensaje ?? "Unknown error occurred.");
                }
            }
            catch (SqlException ex)
            {

                throw new Exception("Se produjo un error al Actualizar Registrar Usuario a nivel BD." + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al realizar Actualizar Usuario." + ex.Message);

            }
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
                RespuestaDapper resp;
                using (var connection = _context.CreateConnection())
                {
                    var multi = await connection.QueryMultipleAsync(
                        sp_ObtenerModulosPorUsuario,
                        new { IdUsuario = idUsuario },
                        commandType: CommandType.StoredProcedure
                    );
                    resp = await multi.ReadFirstOrDefaultAsync<RespuestaDapper>() ?? new RespuestaDapper();
                }
                if (resp != null && resp.Estatus == "S")
                {
                    return JsonSerializer.Deserialize<IEnumerable<ModuloGeneral>>(resp.Objeto) ?? Enumerable.Empty<ModuloGeneral>();
                }
                else
                {
                    throw new InvalidOperationException(resp?.Mensaje ?? "Unknown error occurred.");
                }
            }
            catch (SqlException ex)
            {

                throw new Exception("Se produjo un error al realizar la petición Obtener Modulos PorUsuario a nivel BD." + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al realizar la petición en él método ObtenerModulosPorUsuarioAsync." + ex.Message);

            }
        }

        /// <summary>
        /// Obtiene un usuario por su correo electrónico ejecutando un Stored Procedure.
        /// </summary>
        /// <param name="correo">Correo del usuario a buscar.</param>
        /// <returns>Objeto Usuario si existe, null en caso contrario.</returns>
        public async Task<AccesoUsuario?> ObtenerUsuarioPorEmailAsync(string correo)
        {
            try
            {
                RespuestaDapper resp;
                using (var connection = _context.CreateConnection())
                {
                    var multi = await connection.QueryMultipleAsync(
                        sp_ValidarUsuario,
                        new { CorreoElectronico = correo },
                        commandType: CommandType.StoredProcedure
                    );
                    resp = await multi.ReadFirstOrDefaultAsync<RespuestaDapper>() ?? new RespuestaDapper();
                }
                if (resp != null && resp.Estatus == "S")
                {
                    return JsonSerializer.Deserialize<AccesoUsuario>(resp.Objeto);
                }
                else
                {
                    throw new InvalidOperationException(resp?.Mensaje ?? "Unknown error occurred.");
                }
            }
            catch (SqlException ex)
            {

                throw new Exception("Se produjo un error al Obtener Usuario Por Email a nivel BD." + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al Obtener Usuario Por Email." + ex.Message);

            }
        }

        /// <summary>
        /// Obtiene un usuario por su identificador único.
        /// </summary>
        /// <param name="usuarioId">Id del usuario </param>
        /// <returns></returns>

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
            // using (var connection = _context.CreateConnection())
            // {
            //     var resp = await connection.QueryAsync<Usuario>(
            //         sp_ObtenerUsuarios,
            //         commandType: CommandType.StoredProcedure
            //     );
            //     return resp;
            // }
            try
            {
                RespuestaDapper resp;
                using (var connection = _context.CreateConnection())
                {
                    var multi = await connection.QueryMultipleAsync(
                        sp_ObtenerUsuarios,
                        commandType: CommandType.StoredProcedure
                    );
                    resp = await multi.ReadFirstOrDefaultAsync<RespuestaDapper>() ?? new RespuestaDapper();
                }
                if (resp != null && resp.Estatus == "S")
                {
                    return JsonSerializer.Deserialize<IEnumerable<Usuario>>(resp.Objeto);
                }
                else
                {
                    throw new InvalidOperationException(resp?.Mensaje ?? "Unknown error occurred.");
                }
            }
            catch (SqlException ex)
            {

                throw new Exception("Se produjo un error al Obtener Usuario Por Email a nivel BD." + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al Obtener Usuario Por Email." + ex.Message);

            }
        }

        public async Task<RespuestaDapper> RegistrarUsuarioAsync(Usuario usuario)
        {
            try
            {
                RespuestaDapper resp;
                string responseJson = JsonSerializer.Serialize(usuario);
                using (var connection = _context.CreateConnection())
                {
                    var multi = await connection.QueryMultipleAsync(
                        sp_RegistrarUsuario,
                       new { Json = responseJson },
                        commandType: CommandType.StoredProcedure
                    );
                    resp = await multi.ReadFirstOrDefaultAsync<RespuestaDapper>() ?? new RespuestaDapper();
                }
                if (resp.Estatus != "E")
                {
                    return resp;
                }
                else
                {
                    throw new InvalidOperationException(resp?.Mensaje ?? "Unknown error occurred.");
                }
            }
            catch (SqlException ex)
            {

                throw new Exception("Se produjo un error al realizar Registrar Usuario a nivel BD." + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al realizar Registrar Usuario." + ex.Message);

            }
        }
    }
}
