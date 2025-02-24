using Dominio.Entities;
using Dominio.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Query para obtener un usuario por su correo electrónico.
/// </summary>
public class ObtenerUsuarioPorEmailQuery : IRequest<Usuario?>
{
    public string Email { get; }

    public ObtenerUsuarioPorEmailQuery(string email)
    {
        Email = email;
    }
}

/// <summary>
/// Handler para manejar la consulta de usuario por Email.
/// </summary>
public class ObtenerUsuarioPorEmailHandler : IRequestHandler<ObtenerUsuarioPorEmailQuery, Usuario?>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public ObtenerUsuarioPorEmailHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario?> Handle(ObtenerUsuarioPorEmailQuery request, CancellationToken cancellationToken)
    {
        var usuarios = await _usuarioRepository.ObtenerUsuariosAsync(); // 🔹 Obtener todos los usuarios
        return usuarios.FirstOrDefault(u => u.Email == request.Email); // 🔹 Filtrar por Email
    }
}
