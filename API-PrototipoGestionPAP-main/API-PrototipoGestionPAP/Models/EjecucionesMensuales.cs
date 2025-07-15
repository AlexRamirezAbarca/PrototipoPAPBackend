using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class EjecucionesMensuales
{
    public int EjecucionId { get; set; }

    public int ObraTareaId { get; set; }

    public byte Mes { get; set; }

    public decimal Monto { get; set; }

    public decimal PorcentajeEjecucion { get; set; }

    public string? Descripcion { get; set; }

    public string? Estado { get; set; }

    public int? CreadoPor { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? ModificadoPor { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ObrasTareas ObraTarea { get; set; } = null!;
}
