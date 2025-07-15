using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class Actividades
{
    public int ActividadId { get; set; }

    public int? PlanificacionId { get; set; }

    public int? UnidadRespId { get; set; }

    public int? ObjEstrId { get; set; }

    public int? UnidadEjecutoraId { get; set; }

    public int? ObjetivoOperativoId { get; set; }

    public string Descripcion { get; set; } = null!;

    public int? CarreraId { get; set; }

    public int? FacultadId { get; set; }

    public int? EjePnId { get; set; }

    public int? ObjPnId { get; set; }

    public int? PoliticaPnId { get; set; }

    public int? MetaPnId { get; set; }

    public int? ProgramaNacId { get; set; }

    public int? ProgramaInstId { get; set; }

    public int? ProductoInstId { get; set; }

    public string? Impacto { get; set; }

    public string? Riesgo { get; set; }

    public int? AccionId { get; set; }

    public int? IndicadorId { get; set; }

    public int? ProgramaPreId { get; set; }

    public string? ItemPresupuestario { get; set; }

    public decimal Fuente1Monto { get; set; }

    public decimal Fuente2Monto { get; set; }

    public decimal Fuente3Monto { get; set; }

    public decimal RecursosActividad { get; set; }

    public decimal RecursosRestantes { get; set; }

    public string? Estado { get; set; }

    public int? CreadoPor { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? ModificadoPor { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual Acciones? Accion { get; set; }

    public virtual Carreras? Carrera { get; set; }

    public virtual EjesPlanNacionalDesarrollo? EjePn { get; set; }

    public virtual Facultades? Facultad { get; set; }

    public virtual Indicadores? Indicador { get; set; }

    public virtual MetasPlanNacionalDesarrollo? MetaPn { get; set; }

    public virtual ObjetivosEstrategicosInstitucionales? ObjEstr { get; set; }

    public virtual ObjetivosPlanNacionalDesarrollo? ObjPn { get; set; }

    public virtual ObjetivosOperativos? ObjetivoOperativo { get; set; }

    public virtual ICollection<ObrasTareas> ObrasTareas { get; set; } = new List<ObrasTareas>();

    public virtual PlanificacionesAnuales? Planificacion { get; set; }

    public virtual PoliticasPlanNacionalDesarrollo? PoliticaPn { get; set; }

    public virtual ProductosInstitucionales? ProductoInst { get; set; }

    public virtual ProgramasInstitucionales? ProgramaInst { get; set; }

    public virtual ProgramasNacionales? ProgramaNac { get; set; }

    public virtual ProgramasPresupuestarios? ProgramaPre { get; set; }

    public virtual UnidadesEjecutoras? UnidadEjecutora { get; set; }

    public virtual UnidadesResponsables? UnidadResp { get; set; }
}
