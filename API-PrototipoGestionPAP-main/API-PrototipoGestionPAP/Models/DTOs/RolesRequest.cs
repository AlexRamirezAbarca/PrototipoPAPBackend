using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class RolesRequest
{
    public int RolId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

}

public class RolesPermisosRequest
{
    public RolesRequest Rol { get; set; }
    public ICollection<Permisos> Permisos { get; set; } = new List<Permisos>();
}
