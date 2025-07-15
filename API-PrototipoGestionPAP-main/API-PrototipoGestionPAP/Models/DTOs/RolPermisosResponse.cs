using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class RolPermisosResponse
{
    public int RolPermisoId { get; set; }

    public int? RolId { get; set; }

    public int? PermisoId { get; set; }

    public virtual PermisosResponse? Permiso { get; set; }
}
