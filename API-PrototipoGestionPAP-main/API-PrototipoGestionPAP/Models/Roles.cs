using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class Roles
{
    public int RolId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Estado { get; set; }

    public int? CreadoPor { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? ModificadoPor { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<RolPermisos> RolPermisos { get; set; } = new List<RolPermisos>();

    public virtual ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
}
