using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class Personas
{
    public int PersonaId { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Estado { get; set; }

    public int? CreadoPor { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? ModificadoPor { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
}
