using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class ObrasTareasRequest
{
    public int ObraTareaId { get; set; }

    public int ActividadId { get; set; }

    public string ObraTarea { get; set; } = null!;

    public DateOnly FechaDesde { get; set; }

    public DateOnly FechaHasta { get; set; }

    public string? Estado { get; set; }
}
