using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class PoliticasPlanNacionalDesarrolloResponse
{
    public int PoliticaPnId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Estado { get; set; }
}
