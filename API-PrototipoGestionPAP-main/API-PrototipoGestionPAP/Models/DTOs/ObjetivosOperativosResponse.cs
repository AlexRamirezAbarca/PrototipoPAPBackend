using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class ObjetivosOperativosResponse
{
    public int ObjetivoOperativoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }
}
