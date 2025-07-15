using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class IndicadoresResponse
{
    public int IndicadorId { get; set; }

    public string NombreIndicador { get; set; } = null!;

    public string MetodoCalculo { get; set; } = null!;

    public string MetaIndicador { get; set; } = null!;
}
