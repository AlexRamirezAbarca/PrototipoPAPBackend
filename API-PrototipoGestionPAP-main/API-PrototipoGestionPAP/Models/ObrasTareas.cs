using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class ObrasTareas
{
    public int ObraTareaId { get; set; }

    public int ActividadId { get; set; }

    public string ObraTarea { get; set; } = null!;

    public DateOnly FechaDesde { get; set; }

    public DateOnly FechaHasta { get; set; }

    public string? Estado { get; set; }

    public int? CreadoPor { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? ModificadoPor { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual Actividades Actividad { get; set; } = null!;

    public virtual ICollection<EjecucionesMensuales> EjecucionesMensuales { get; set; } = new List<EjecucionesMensuales>();
}
