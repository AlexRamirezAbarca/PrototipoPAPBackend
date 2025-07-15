using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class RegistroSesiones
{
    public int RegistroSesionId { get; set; }

    public int UsuarioId { get; set; }

    public DateTime FechaInicio { get; set; }

    public string DireccionIp { get; set; } = null!;

    public string? Dispositivo { get; set; }

    public string? Navegador { get; set; }

    public string ResultadoSesion { get; set; } = null!;

    public string? MensajeError { get; set; }

    public virtual Usuarios Usuario { get; set; } = null!;
}
