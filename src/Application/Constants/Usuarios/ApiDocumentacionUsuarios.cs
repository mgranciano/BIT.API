namespace Application.Constants;

/// <summary>
/// Contiene los textos de documentación para el Controlador de Usuarios.
/// </summary>
public static class ApiDocumentacionUsuarios
{
    public const string ObtenerUsuariosSummary = "Obtiene la lista de usuarios";
    public const string ObtenerUsuariosDescription = "Recupera todos los usuarios registrados en el sistema.";
    public const string ObtenerUsuarioSummary = "Obtiene un usuario por ID";
    public const string ObtenerUsuarioDescription = "Recupera los datos de un usuario específico.";
    public const string AltaUsuarioSummary = "Registra un nuevo usuario";
    public const string AltaUsuarioDescription = "Crea un nuevo usuario en el sistema.";
    public const string ActualizarUsuarioSummary = "Actualiza parcialmente un usuario";
    public const string ActualizarUsuarioDescription = "Modifica solo los datos enviados del usuario.";
    public const string EliminarUsuarioSummary = "Elimina un usuario";
    public const string EliminarUsuarioDescription = "Cambia el estado del usuario a inactivo.";
}
