using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class PermisosResponse
{
    public int PermisoId { get; set; }

    public string Codigo { get; set; } = null!;

    public string? Descripcion { get; set; }
}