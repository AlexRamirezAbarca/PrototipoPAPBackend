using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class MetasPlanNacionalDesarrolloResponse
{
    public int MetaPnId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }
}
