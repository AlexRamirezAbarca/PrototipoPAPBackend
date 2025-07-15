using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Text; // Para StringBuilder
using API_PrototipoGestionPAP.Models;
using System;
using System.Collections.Generic;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using System.IO;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly DBContext _context;

        public ReportesController(DBContext context)
        {
            _context = context;
        }

        // -----------------------------------------------------------
        // ENDPOINT 1: REPORTE "TODAS LAS PLANIFICACIONES"
        // -----------------------------------------------------------
        [HttpGet("TodasPlanificacionesPdf")]
        public async Task<IActionResult> ObtenerTodasPlanificacionesPdf([FromQuery] int? anio)
        {
            // Consulta de planificaciones
            var consultaPlanificaciones = _context.PlanificacionesAnuales
                .Where(p => !anio.HasValue || p.Anio == anio.Value)
                .Select(p => new
                {
                    p.PlanificacionId,
                    p.Anio,
                    p.Descripcion,
                    CantidadActividades = p.Actividades.Count(),
                    PresupuestoTotal = p.Actividades.Sum(a => (decimal?)a.RecursosActividad) ?? 0,
                    FechaCreacion = p.FechaCreacion.Value.ToString("dd/MM/yyyy")
                })
                .OrderBy(p => p.Anio);

            var planificaciones = await consultaPlanificaciones.ToListAsync();

            // Construir HTML
            var sb = new StringBuilder();
            ConstruirEncabezadoHtml(sb, "Reporte: Todas las Planificaciones");

            sb.AppendLine("    <div class=\"report-container\">");
            sb.AppendLine("        <div class=\"report-info\">");
            sb.AppendLine("            <h3>Reporte de Todas las Planificaciones</h3>");
            var fecha = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Guayaquil"));
            sb.AppendLine($"           <p><strong>Fecha de Generación:</strong> {fecha:dd/MM/yyyy}</p>");

            if (anio.HasValue)
            {
                sb.AppendLine($"           <p><strong>Filtrado por Año:</strong> {anio}</p>");
            }
            else
            {
                sb.AppendLine("           <p>Mostrando todas las planificaciones, sin filtrar por año.</p>");
            }
            sb.AppendLine("        </div>");

            // Tabla de planificaciones
            sb.AppendLine("        <div class=\"report-section\">");
            sb.AppendLine("            <table class=\"table table-bordered table-striped\">");
            sb.AppendLine("                <thead>");
            sb.AppendLine("                    <tr>");
            sb.AppendLine("                        <th>ID</th>");
            sb.AppendLine("                        <th>Año</th>");
            sb.AppendLine("                        <th>Descripción</th>");
            sb.AppendLine("                        <th>Actividades</th>");
            sb.AppendLine("                        <th>Presupuesto Total</th>");
            sb.AppendLine("                        <th>Fecha Creación</th>");
            sb.AppendLine("                    </tr>");
            sb.AppendLine("                </thead>");
            sb.AppendLine("                <tbody>");

            foreach (var p in planificaciones)
            {
                sb.AppendLine("                    <tr>");
                sb.AppendLine($"                       <td>{p.PlanificacionId}</td>");
                sb.AppendLine($"                       <td>{p.Anio}</td>");
                sb.AppendLine($"                       <td>{p.Descripcion}</td>");
                sb.AppendLine($"                       <td>{p.CantidadActividades}</td>");
                sb.AppendLine($"                       <td>{p.PresupuestoTotal:0.00}</td>");
                sb.AppendLine($"                       <td>{p.FechaCreacion}</td>");
                sb.AppendLine("                    </tr>");
            }

            sb.AppendLine("                </tbody>");
            sb.AppendLine("            </table>");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>"); // .report-container

            ConstruirPieHtml(sb);
            return ConstruirResultadoPdf(sb, "Reporte_Todas_Planificaciones.pdf");
        }

        // -----------------------------------------------------------
        // ENDPOINT 2: DETALLE DE UNA PLANIFICACIÓN
        // -----------------------------------------------------------
        [HttpGet("DetallePlanificacionPdf")]
        public async Task<IActionResult> ObtenerDetallePlanificacionPdf([FromQuery] int planificacionId)
        {
            var planificacion = await _context.PlanificacionesAnuales
                .Where(p => p.PlanificacionId == planificacionId)
                .Select(p => new
                {
                    p.PlanificacionId,
                    p.Anio,
                    p.Descripcion,
                    CantidadActividades = p.Actividades.Count(),
                    PresupuestoPlanificado = p.Actividades.Sum(a => (decimal?)a.RecursosActividad) ?? 0,
                    TotalEjecutado = p.Actividades
                        .SelectMany(a => a.ObrasTareas)
                        .SelectMany(o => o.EjecucionesMensuales)
                        .Sum(e => (decimal?)e.Monto) ?? 0,
                    p.Estado,
                    FechaCreacion = p.FechaCreacion.Value.ToString("dd/MM/yyyy")
                })
                .FirstOrDefaultAsync();

            if (planificacion == null)
                return NotFound($"No se encontró la planificación con ID {planificacionId}.");

            var sb = new StringBuilder();
            ConstruirEncabezadoHtml(sb, "Reporte: Detalle de Planificación");

            sb.AppendLine("    <div class=\"report-container\">");
            sb.AppendLine("        <div class=\"report-info\">");
            sb.AppendLine($"           <h3>Detalle de la Planificación (ID: {planificacionId})</h3>");
            var fecha = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Guayaquil"));
            sb.AppendLine($"           <p><strong>Fecha de Generación:</strong> {fecha:dd/MM/yyyy}</p>");
            sb.AppendLine("        </div>");

            // Tabla principal de detalle
            sb.AppendLine("        <div class=\"report-section\">");
            sb.AppendLine("            <table class=\"table table-bordered table-striped\">");
            sb.AppendLine("                <thead>");
            sb.AppendLine("                    <tr>");
            sb.AppendLine("                        <th>ID Planificación</th>");
            sb.AppendLine("                        <th>Año</th>");
            sb.AppendLine("                        <th>Descripción</th>");
            sb.AppendLine("                        <th>Actividades</th>");
            sb.AppendLine("                        <th>Presupuesto Planificado</th>");
            sb.AppendLine("                        <th>Total Ejecutado</th>");
            sb.AppendLine("                        <th>Estado</th>");
            sb.AppendLine("                        <th>Fecha Creación</th>");
            sb.AppendLine("                    </tr>");
            sb.AppendLine("                </thead>");
            sb.AppendLine("                <tbody>");
            sb.AppendLine("                    <tr>");
            sb.AppendLine($"                       <td>{planificacion.PlanificacionId}</td>");
            sb.AppendLine($"                       <td>{planificacion.Anio}</td>");
            sb.AppendLine($"                       <td>{planificacion.Descripcion}</td>");
            sb.AppendLine($"                       <td>{planificacion.CantidadActividades}</td>");
            sb.AppendLine($"                       <td>{planificacion.PresupuestoPlanificado:0.00}</td>");
            sb.AppendLine($"                       <td>{planificacion.TotalEjecutado:0.00}</td>");
            sb.AppendLine($"                       <td>{planificacion.Estado}</td>");
            sb.AppendLine($"                       <td>{planificacion.FechaCreacion}</td>");
            sb.AppendLine("                    </tr>");
            sb.AppendLine("                </tbody>");
            sb.AppendLine("            </table>");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>"); // .report-container

            ConstruirPieHtml(sb);
            return ConstruirResultadoPdf(sb, $"Detalle_Planificacion_{planificacionId}.pdf");
        }

        // -----------------------------------------------------------
        // ENDPOINT 3: ACTIVIDADES POR PLANIFICACIÓN
        // -----------------------------------------------------------
        [HttpGet("ActividadesPorPlanificacionPdf")]
        public async Task<IActionResult> ObtenerActividadesPorPlanificacionPdf([FromQuery] int planificacionId)
        {
            var planificacion = await _context.PlanificacionesAnuales
                .Where(p => p.PlanificacionId == planificacionId)
                .Select(p => new { p.Anio })
                .FirstOrDefaultAsync();

            if (planificacion == null)
                return NotFound($"No se encontró la planificación con ID {planificacionId}.");

            var actividades = await _context.Actividades
                .Where(a => a.PlanificacionId == planificacionId)
                .Select(a => new
                {
                    a.ActividadId,
                    a.Descripcion,
                    UnidadResponsable = a.UnidadResp.Nombre,
                    UnidadEjecutora = a.UnidadEjecutora.Nombre,
                    Presupuesto = a.RecursosActividad,
                    MontoEjecutado = a.ObrasTareas
                        .SelectMany(o => o.EjecucionesMensuales)
                        .Sum(e => (decimal?)e.Monto) ?? 0
                })
                .ToListAsync();

            var sb = new StringBuilder();
            ConstruirEncabezadoHtml(sb, "Reporte: Actividades por Planificación");

            sb.AppendLine("    <div class=\"report-container\">");
            sb.AppendLine("        <div class=\"report-info\">");
            sb.AppendLine($"           <h3>Actividades de la Planificación (ID: {planificacionId}, Año: {planificacion.Anio})</h3>");
            var fecha = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Guayaquil"));
            sb.AppendLine($"           <p><strong>Fecha de Generación:</strong> {fecha:dd/MM/yyyy}</p>");
            sb.AppendLine("        </div>");

            sb.AppendLine("        <div class=\"report-section\">");
            sb.AppendLine("            <table class=\"table table-bordered table-striped\">");
            sb.AppendLine("                <thead>");
            sb.AppendLine("                    <tr>");
            sb.AppendLine("                        <th>ID Actividad</th>");
            sb.AppendLine("                        <th>Descripción</th>");
            sb.AppendLine("                        <th>Unidad Responsable</th>");
            sb.AppendLine("                        <th>Unidad Ejecutora</th>");
            sb.AppendLine("                        <th>Presupuesto</th>");
            sb.AppendLine("                        <th>Monto Ejecutado</th>");
            sb.AppendLine("                        <th>% Ejecución</th>");
            sb.AppendLine("                    </tr>");
            sb.AppendLine("                </thead>");
            sb.AppendLine("                <tbody>");

            foreach (var act in actividades)
            {
                var porcentaje = act.Presupuesto > 0
                    ? Math.Round((act.MontoEjecutado / act.Presupuesto) * 100, 2)
                    : 0;

                sb.AppendLine("                    <tr>");
                sb.AppendLine($"                       <td>{act.ActividadId}</td>");
                sb.AppendLine($"                       <td>{act.Descripcion}</td>");
                sb.AppendLine($"                       <td>{act.UnidadResponsable}</td>");
                sb.AppendLine($"                       <td>{act.UnidadEjecutora}</td>");
                sb.AppendLine($"                       <td>{act.Presupuesto:0.00}</td>");
                sb.AppendLine($"                       <td>{act.MontoEjecutado:0.00}</td>");
                sb.AppendLine($"                       <td>{porcentaje}%</td>");
                sb.AppendLine("                    </tr>");
            }

            sb.AppendLine("                </tbody>");
            sb.AppendLine("            </table>");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");

            ConstruirPieHtml(sb);
            return ConstruirResultadoPdf(sb, $"Actividades_Planif_{planificacionId}.pdf");
        }

        // -----------------------------------------------------------
        // ENDPOINT 4: EJECUCIÓN PRESUPUESTARIA (PLAN VS. REAL)
        // -----------------------------------------------------------
        [HttpGet("PlanVsRealPdf")]
        public async Task<IActionResult> ObtenerPlanVsRealPdf([FromQuery] int planificacionId)
        {
            var planificacion = await _context.PlanificacionesAnuales
                .Where(p => p.PlanificacionId == planificacionId)
                .Select(p => new { p.Anio })
                .FirstOrDefaultAsync();

            if (planificacion == null)
                return NotFound($"No se encontró la planificación con ID {planificacionId}.");

            var datos = await _context.Actividades
                .Where(a => a.PlanificacionId == planificacionId)
                .Select(a => new
                {
                    a.ActividadId,
                    a.Descripcion,
                    PresupuestoPlanificado = a.RecursosActividad,
                    MontoEjecutado = a.ObrasTareas
                        .SelectMany(o => o.EjecucionesMensuales)
                        .Sum(e => (decimal?)e.Monto) ?? 0
                })
                .ToListAsync();

            var sb = new StringBuilder();
            ConstruirEncabezadoHtml(sb, "Reporte: Ejecución Presupuestaria (Plan vs. Real)");

            sb.AppendLine("    <div class=\"report-container\">");
            sb.AppendLine("        <div class=\"report-info\">");
            sb.AppendLine($"           <h3>Ejecución Presupuestaria (Plan vs. Real) - Planificación ID: {planificacionId}, Año: {planificacion.Anio}</h3>");
            var fecha = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Guayaquil"));
            sb.AppendLine($"           <p><strong>Fecha de Generación:</strong> {fecha:dd/MM/yyyy}</p>");
            sb.AppendLine("        </div>");

            // Tabla con la ejecución plan vs. real
            sb.AppendLine("        <div class=\"report-section\">");
            sb.AppendLine("            <table class=\"table table-bordered table-striped\">");
            sb.AppendLine("                <thead>");
            sb.AppendLine("                    <tr>");
            sb.AppendLine("                        <th>ID Actividad</th>");
            sb.AppendLine("                        <th>Descripción</th>");
            sb.AppendLine("                        <th>Presupuesto Planificado</th>");
            sb.AppendLine("                        <th>Monto Ejecutado</th>");
            sb.AppendLine("                        <th>Diferencia</th>");
            sb.AppendLine("                        <th>% Ejecución</th>");
            sb.AppendLine("                    </tr>");
            sb.AppendLine("                </thead>");
            sb.AppendLine("                <tbody>");

            foreach (var item in datos)
            {
                var diferencia = item.PresupuestoPlanificado - item.MontoEjecutado;
                var porcentaje = item.PresupuestoPlanificado > 0
                    ? Math.Round((item.MontoEjecutado / item.PresupuestoPlanificado) * 100, 2)
                    : 0;

                sb.AppendLine("                    <tr>");
                sb.AppendLine($"                       <td>{item.ActividadId}</td>");
                sb.AppendLine($"                       <td>{item.Descripcion}</td>");
                sb.AppendLine($"                       <td>{item.PresupuestoPlanificado:0.00}</td>");
                sb.AppendLine($"                       <td>{item.MontoEjecutado:0.00}</td>");
                sb.AppendLine($"                       <td>{diferencia:0.00}</td>");
                sb.AppendLine($"                       <td>{porcentaje}%</td>");
                sb.AppendLine("                    </tr>");
            }

            sb.AppendLine("                </tbody>");
            sb.AppendLine("            </table>");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");

            ConstruirPieHtml(sb);
            return ConstruirResultadoPdf(sb, $"PlanVsReal_{planificacionId}.pdf");
        }

        // -----------------------------------------------------------
        // ENDPOINT 5: OBRAS/TAREAS Y CRONOGRAMA
        // -----------------------------------------------------------
        [HttpGet("ObrasTareasCronogramaPdf")]
        public async Task<IActionResult> ObtenerObrasTareasCronogramaPdf([FromQuery] int planificacionId)
        {
            var planificacion = await _context.PlanificacionesAnuales
                .Where(p => p.PlanificacionId == planificacionId)
                .Select(p => new { p.Anio })
                .FirstOrDefaultAsync();

            if (planificacion == null)
                return NotFound($"No se encontró la planificación con ID {planificacionId}.");

            var obrasTareas = await _context.Actividades
                .Where(a => a.PlanificacionId == planificacionId)
                .SelectMany(a => a.ObrasTareas, (a, ot) => new
                {
                    ot.ObraTareaId,
                    Actividad = a.Descripcion,
                    ot.ObraTarea,
                    FechaInicio = ot.FechaDesde,
                    FechaFin = ot.FechaHasta,
                    ot.Estado
                })
                .OrderBy(x => x.Actividad)
                .ThenBy(x => x.FechaInicio)
                .ToListAsync();

            var sb = new StringBuilder();
            ConstruirEncabezadoHtml(sb, "Reporte: Obras/Tareas y Cronograma");

            sb.AppendLine("    <div class=\"report-container\">");
            sb.AppendLine("        <div class=\"report-info\">");
            sb.AppendLine($"           <h3>Obras/Tareas - Planificación (ID: {planificacionId}, Año: {planificacion.Anio})</h3>");
            var fecha = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Guayaquil"));
            sb.AppendLine($"           <p><strong>Fecha de Generación:</strong> {fecha:dd/MM/yyyy}</p>");
            sb.AppendLine("        </div>");

            sb.AppendLine("        <div class=\"report-section\">");
            sb.AppendLine("            <table class=\"table table-bordered table-striped\">");
            sb.AppendLine("                <thead>");
            sb.AppendLine("                    <tr>");
            sb.AppendLine("                        <th>ID Obra/Tarea</th>");
            sb.AppendLine("                        <th>Actividad</th>");
            sb.AppendLine("                        <th>Obra/Tarea</th>");
            sb.AppendLine("                        <th>Fecha Inicio</th>");
            sb.AppendLine("                        <th>Fecha Fin</th>");
            sb.AppendLine("                    </tr>");
            sb.AppendLine("                </thead>");
            sb.AppendLine("                <tbody>");

            foreach (var ot in obrasTareas)
            {
                sb.AppendLine("                    <tr>");
                sb.AppendLine($"                       <td>{ot.ObraTareaId}</td>");
                sb.AppendLine($"                       <td>{ot.Actividad}</td>");
                sb.AppendLine($"                       <td>{ot.ObraTarea}</td>");
                sb.AppendLine($"                       <td>{ot.FechaInicio:dd/MM/yyyy}</td>");
                sb.AppendLine($"                       <td>{ot.FechaFin:dd/MM/yyyy}</td>");
                sb.AppendLine("                    </tr>");
            }

            sb.AppendLine("                </tbody>");
            sb.AppendLine("            </table>");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");

            ConstruirPieHtml(sb);
            return ConstruirResultadoPdf(sb, $"ObrasTareas_{planificacionId}.pdf");
        }

        // -----------------------------------------------------------
        // ENDPOINT 6: REPORTE CONSOLIDADO DE AVANCE MENSUAL
        // -----------------------------------------------------------
        [HttpGet("AvanceMensualConsolidadoPdf")]
        public async Task<IActionResult> ObtenerAvanceMensualConsolidadoPdf([FromQuery] int planificacionId)
        {
            var planificacion = await _context.PlanificacionesAnuales
                .Where(p => p.PlanificacionId == planificacionId)
                .Select(p => new { p.Anio })
                .FirstOrDefaultAsync();

            if (planificacion == null)
                return NotFound($"No se encontró la planificación con ID {planificacionId}.");

            // Total planificado
            decimal totalPlanificado = await _context.Actividades
                .Where(a => a.PlanificacionId == planificacionId)
                .SumAsync(a => (decimal?)a.RecursosActividad) ?? 0;

            // Ejecución agrupada por mes
            var datosMensuales = await _context.Actividades
                .Where(a => a.PlanificacionId == planificacionId)
                .SelectMany(a => a.ObrasTareas)
                .SelectMany(ot => ot.EjecucionesMensuales)
                .GroupBy(em => em.Mes)
                .Select(g => new
                {
                    Mes = g.Key,
                    TotalMes = g.Sum(x => x.Monto)
                })
                .OrderBy(x => x.Mes)
                .ToListAsync();

            var sb = new StringBuilder();
            ConstruirEncabezadoHtml(sb, "Reporte: Avance Mensual Consolidado");

            sb.AppendLine("    <div class=\"report-container\">");
            sb.AppendLine("        <div class=\"report-info\">");
            sb.AppendLine($"           <h3>Avance Mensual Consolidado - Planificación (ID: {planificacionId}, Año: {planificacion.Anio})</h3>");
            var fecha = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Guayaquil"));
            sb.AppendLine($"           <p><strong>Fecha de Generación:</strong> {fecha:dd/MM/yyyy}</p>");
            sb.AppendLine("        </div>");

            // Tabla con la ejecución mensual
            sb.AppendLine("        <div class=\"report-section\">");
            sb.AppendLine("            <table class=\"table table-bordered table-striped\">");
            sb.AppendLine("                <thead>");
            sb.AppendLine("                    <tr>");
            sb.AppendLine("                        <th>Mes</th>");
            sb.AppendLine("                        <th>Total Ejecutado en el Mes</th>");
            sb.AppendLine("                        <th>% Respecto al Plan Anual</th>");
            sb.AppendLine("                    </tr>");
            sb.AppendLine("                </thead>");
            sb.AppendLine("                <tbody>");

            foreach (var item in datosMensuales)
            {
                decimal porcentaje = totalPlanificado > 0
                    ? Math.Round((item.TotalMes / totalPlanificado) * 100, 2)
                    : 0;

                sb.AppendLine("                    <tr>");
                sb.AppendLine($"                       <td>{item.Mes} ({NombreMes(item.Mes)})</td>");
                sb.AppendLine($"                       <td>{item.TotalMes:0.00}</td>");
                sb.AppendLine($"                       <td>{porcentaje}%</td>");
                sb.AppendLine("                    </tr>");
            }

            sb.AppendLine("                </tbody>");
            sb.AppendLine("            </table>");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");

            ConstruirPieHtml(sb);
            return ConstruirResultadoPdf(sb, $"AvanceMensual_{planificacionId}.pdf");
        }

        // =========================================================================
        // MÉTODOS AUXILIARES
        // =========================================================================

        private IActionResult ConstruirResultadoPdf(StringBuilder sb, string nombreArchivo)
        {
            var pdfBytes = ConvertirHtmlAPdf(sb.ToString());

            // 1) Creamos un FileContentResult sin el FileDownloadName (tercer parámetro)
            var fileContentResult = new FileContentResult(pdfBytes, "application/pdf");

            // 2) Ajustamos la cabecera Content-Disposition a "inline"
            //    para que el navegador intente mostrarlo embebido
            Response.Headers["Content-Disposition"] = $"inline; filename={nombreArchivo}";

            return fileContentResult;
        }


        /// <summary>
        /// Construye la cabecera HTML con los estilos y el encabezado (logo).
        /// </summary>
        private void ConstruirEncabezadoHtml(StringBuilder sb, string tituloPagina)
        {
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"es\">");
            sb.AppendLine("<head>");
            sb.AppendLine("    <meta charset=\"utf-8\" />");
            sb.AppendLine($"    <title>{tituloPagina}</title>");
            sb.AppendLine("    <link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" />");
            sb.AppendLine("    <style>");
            // Regla @page para remover márgenes en el PDF.
            sb.AppendLine("        @page { margin: 0; }");
            sb.AppendLine("        body { font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; color: #000; }");
            sb.AppendLine("        .report-header { display: flex; align-items: center; background-color: #004383; color: #fff; padding: 10px 20px; margin-bottom: 20px; }");
            sb.AppendLine("        .report-header img { height: 60px; margin-right: 20px; }");
            sb.AppendLine("        .report-header .header-texts { display: flex; flex-direction: column; }");
            sb.AppendLine("        .report-header .header-texts h1 { margin: 0; font-size: 1.6rem; font-weight: bold; }");
            sb.AppendLine("        .report-header .header-texts h2 { margin: 0; font-size: 1rem; font-weight: normal; }");
            sb.AppendLine("        .report-container { background-color: #fff; margin: 0 20px; padding: 20px; box-shadow: 0 4px 5px rgba(0, 0, 0, 0.3); }");
            sb.AppendLine("        .report-info h3 { color: #004383; font-size: 1.2rem; margin-bottom: 10px; }");
            sb.AppendLine("        .report-info p { margin: 0.25rem 0; }");
            sb.AppendLine("        .report-section { margin-top: 30px; }");
            sb.AppendLine("        .report-section h4 { color: #004383; margin-bottom: 15px; font-size: 1.1rem; font-weight: bold; }");
            sb.AppendLine("        .table { width: 100%; margin-bottom: 1rem; background-color: transparent; }");
            sb.AppendLine("        .table thead { background-color: #f8f9fa; }");
            sb.AppendLine("        .table thead th { text-align: center; font-weight: bold; vertical-align: middle; }");
            sb.AppendLine("        .table tbody td { vertical-align: middle; }");
            sb.AppendLine("        .report-footer { margin-top: 30px; text-align: center; color: #6c757d; font-size: 0.9rem; }");
            sb.AppendLine("    </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");

            // Encabezado institucional
            sb.AppendLine("    <div class=\"report-header\">");
            sb.AppendLine("    <svg width=\"100\" height=\"60\" xmlns=\"http://www.w3.org/2000/svg\">");
            sb.AppendLine("        <defs>");
            sb.AppendLine("            <filter id=\"invert\">");
            sb.AppendLine("                <feColorMatrix in=\"SourceGraphic\" type=\"matrix\" values=\"");
            sb.AppendLine("                    -1  0  0  0  1");
            sb.AppendLine("                     0 -1  0  0  1");
            sb.AppendLine("                     0  0 -1  0  1");
            sb.AppendLine("                     0  0  0  1  0\"/>");
            sb.AppendLine("            </filter>");
            sb.AppendLine("        </defs>");
            sb.AppendLine("        <image filter=\"url(#invert)\" width=\"100\" height=\"60\" preserveAspectRatio=\"xMidYMid meet\" xlink:href=\"https://upload.wikimedia.org/wikipedia/commons/6/6d/LogoUGcolor.png\"/>");
            sb.AppendLine("    </svg>");
            sb.AppendLine("        <div class=\"header-texts\">");
            sb.AppendLine("            <h1>Universidad de Guayaquil</h1>");
            sb.AppendLine("            <h2>Plan Operativo Anual</h2>");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");
        }


        /// <summary>
        /// Construye el pie de página e incluye script de Bootstrap (opcional).
        /// </summary>
        private void ConstruirPieHtml(StringBuilder sb)
        {
            sb.AppendLine("    <div class=\"report-footer\">");
            sb.AppendLine("        <hr />");
            sb.AppendLine("        <p>Reporte generado automáticamente por el Sistema de Gestión PAP - Universidad de Guayaquil.</p>");
            sb.AppendLine("        <p>© 2025 Universidad de Guayaquil. Todos los derechos reservados.</p>");
            sb.AppendLine("    </div>");
            sb.AppendLine("    <script src=\"https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js\"></script>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
        }

        /// <summary>
        /// Método auxiliar para obtener el nombre del mes según el número (1-12).
        /// </summary>
        private string NombreMes(int mes)
        {
            return mes switch
            {
                1 => "Enero",
                2 => "Febrero",
                3 => "Marzo",
                4 => "Abril",
                5 => "Mayo",
                6 => "Junio",
                7 => "Julio",
                8 => "Agosto",
                9 => "Septiembre",
                10 => "Octubre",
                11 => "Noviembre",
                12 => "Diciembre",
                _ => ""
            };
        }

        private byte[] ConvertirHtmlAPdf(string contenidoHtml)
        {
            using var ms = new MemoryStream();
            using var writer = new PdfWriter(ms);
            using var pdfDoc = new PdfDocument(writer);

            // Crea el objeto Document y elimina sus márgenes.
            using var document = new iText.Layout.Document(pdfDoc);
            document.SetMargins(0, 0, 0, 0);

            var propiedades = new ConverterProperties();
            // Realiza la conversión del HTML a PDF
            HtmlConverter.ConvertToPdf(contenidoHtml, pdfDoc, propiedades);

            // Es importante cerrar el document para que se apliquen todos los cambios
            document.Close();
            return ms.ToArray();
        }

    }
}
