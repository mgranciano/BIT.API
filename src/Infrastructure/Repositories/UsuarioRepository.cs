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
        /// Obtiene un usuario por su correo electrónico ejecutando un Stored Procedure.
        /// </summary>
        /// <param name="correo">Correo del usuario a buscar.</param>
        /// <returns>Objeto Usuario si existe, null en caso contrario.</returns>
        public async Task<LogInUsuario?> ObtenerUsuarioPorCorreoAsync(string correo)
        {

            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<LogInUsuario>(
                sp_ValidarUsuario,
                new { Correo = correo },
                commandType: CommandType.StoredProcedure
            );
        }


        public Task<Usuario?> ObtenerUsuarioPorIdAsync(string usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> ObtenerUsuariosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> RegistrarUsuarioAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
