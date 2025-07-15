using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class UsuariosRequest
{
    public int UsuarioId { get; set; }

    public int? PersonaId { get; set; }

    public int? RolId { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string? Contraseña { get; set; }

}

public class CrearUsuarioConPersonaRequest
{
    public UsuariosRequest Usuario { get; set; }

    public PersonasRequest Persona { get; set; }
}

