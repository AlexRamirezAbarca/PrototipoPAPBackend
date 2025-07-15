﻿using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class EjecucionesMensualesRequest
{
    public int EjecucionId { get; set; }

    public int ObraTareaId { get; set; }

    public byte Mes { get; set; }

    public decimal Monto { get; set; }

    public decimal PorcentajeEjecucion { get; set; }

    public string? Descripcion { get; set; }

}
