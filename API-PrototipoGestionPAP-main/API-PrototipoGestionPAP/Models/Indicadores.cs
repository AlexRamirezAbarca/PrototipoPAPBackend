using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class Indicadores
{
    public int IndicadorId { get; set; }

    public string NombreIndicador { get; set; } = null!;

    public string MetodoCalculo { get; set; } = null!;

    public string MetaIndicador { get; set; } = null!;

    public string? Estado { get; set; }

    public int? CreadoPor { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? ModificadoPor { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<Actividades> Actividades { get; set; } = new List<Actividades>();
}
