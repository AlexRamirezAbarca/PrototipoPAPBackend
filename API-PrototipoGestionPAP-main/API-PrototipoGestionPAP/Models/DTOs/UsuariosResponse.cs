using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class UsuariosResponse
{
    public int UsuarioId { get; set; }

    public int? PersonaId { get; set; }

    public int? RolId { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public RolesResponse Rol { get; set; }
    public PersonasResponse Persona { get; set; }

}
