using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class UnidadesEjecutoras
{
    public int UnidadEjecutoraId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Estado { get; set; }

    public int? CreadoPor { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? ModificadoPor { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<Actividades> Actividades { get; set; } = new List<Actividades>();
}
