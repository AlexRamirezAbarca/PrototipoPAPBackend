using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class PersonasRequest
{
    public int PersonaId { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public string? Telefono { get; set; }


}

public partial class PersonasUsuarioRequest
{
    public int PersonaId { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<UsuariosRequest> Usuarios { get; set; } = new List<UsuariosRequest>();

}