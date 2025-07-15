using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class UnidadesEjecutorasResponse
{
    public int UnidadEjecutoraId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }
}
