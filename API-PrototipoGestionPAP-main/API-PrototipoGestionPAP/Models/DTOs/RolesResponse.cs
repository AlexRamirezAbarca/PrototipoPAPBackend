using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class RolesResponse
{
    public int RolId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<PermisosResponse> Permisos { get; set; } = new List<PermisosResponse>();

}
