using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class Usuarios
{
    public int UsuarioId { get; set; }

    public int? PersonaId { get; set; }

    public int? RolId { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string? Estado { get; set; }

    public int? CreadoPor { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? ModificadoPor { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual Personas? Persona { get; set; }

    public virtual ICollection<RegistroSesiones> RegistroSesiones { get; set; } = new List<RegistroSesiones>();

    public virtual Roles? Rol { get; set; }
}
