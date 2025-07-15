using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjecucionesMensualesController : ControllerBase
    {
        private readonly DBContext _context;

        public EjecucionesMensualesController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetEjecucionesMensuales(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string filter = null,
            [FromQuery] string filterField = null)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page y PageSize deben ser mayores a 0.");
            }

            IQueryable<EjecucionesMensuales> query = _context.EjecucionesMensuales
                .Where(x => x.Estado != "N")
                .Include(x => x.ObraTarea);

            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterField))
            {
                PropertyInfo property = typeof(EjecucionesMensuales).GetProperty(filterField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                {
                    return BadRequest($"No se encontró la propiedad '{filterField}' en EjecucionesMensuales.");
                }

                if (property.PropertyType != typeof(string))
                {
                    return BadRequest($"La propiedad '{filterField}' no es de tipo string y no se puede aplicar un filtro de texto.");
                }

                var parameter = Expression.Parameter(typeof(EjecucionesMensuales), "x");
                var propertyAccess = Expression.Property(parameter, property);
                var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var filterExpression = Expression.Call(propertyAccess, method, Expression.Constant(filter, typeof(string)));
                var lambda = Expression.Lambda<Func<EjecucionesMensuales, bool>>(filterExpression, parameter);

                query = query.Where(lambda);
            }
            else if (!string.IsNullOrEmpty(filter) || !string.IsNullOrEmpty(filterField))
            {
                return BadRequest("Ambos parámetros 'filter' y 'filterField' deben ser proporcionados para aplicar un filtro.");
            }

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            if (page > totalPages && totalPages > 0)
            {
                return BadRequest("La página solicitada excede el total de páginas disponibles.");
            }

            var ejecuciones = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new
            {
                Data = ejecuciones,
                Pagination = new
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    TotalRecords = totalRecords
                }
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EjecucionesMensuales>> GetEjecucionesMensuales(int id)
        {
            var ejecucion = await _context.EjecucionesMensuales
                .Include(x => x.ObraTarea)
                .FirstOrDefaultAsync(x => x.EjecucionId == id);

            if (ejecucion == null || ejecucion.Estado == "N")
            {
                return NotFound();
            }

            return ejecucion;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEjecucionesMensuales(int id, EjecucionesMensualesRequest request)
        {
            var existingEntity = await _context.EjecucionesMensuales.FindAsync(id);
            if (existingEntity == null || existingEntity.Estado == "N")
            {
                return NotFound();
            }

            if (request.EjecucionId != id)
            {
                return BadRequest("El identificador de la ejecución no coincide.");
            }
            var obraTarea = await _context.ObrasTareas
            .Include(o => o.Actividad)
            .FirstOrDefaultAsync(o => o.ObraTareaId == existingEntity.ObraTareaId);


            request.PorcentajeEjecucion = (request.Monto / obraTarea.Actividad.RecursosActividad) * 100;

            UniversalMapper.Map(request, existingEntity);
            existingEntity.FechaModificacion = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EjecucionMensualesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            await UpdateActivityResourcesAsync(existingEntity.ObraTareaId);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EjecucionesMensuales>> PostEjecucionesMensuales(EjecucionesMensualesRequest request)
        {
            var ejecucion = UniversalMapper.Map<EjecucionesMensualesRequest, EjecucionesMensuales>(request);
            ejecucion.Estado = "A";
            ejecucion.FechaCreacion = DateTime.UtcNow;

            var obraTarea = await _context.ObrasTareas
            .Include(o => o.Actividad)
            .FirstOrDefaultAsync(o => o.ObraTareaId == request.ObraTareaId);



            request.PorcentajeEjecucion = (request.Monto / obraTarea.Actividad.RecursosActividad) * 100;

            _context.EjecucionesMensuales.Add(ejecucion);
            await _context.SaveChangesAsync();

            await UpdateActivityResourcesAsync(ejecucion.ObraTareaId);

            return CreatedAtAction(nameof(GetEjecucionesMensuales), new { id = ejecucion.EjecucionId }, ejecucion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEjecucionesMensuales(int id)
        {
            var ejecucion = await _context.EjecucionesMensuales.FindAsync(id);
            if (ejecucion == null || ejecucion.Estado == "N")
            {
                return NotFound();
            }

            ejecucion.Estado = "N";
            ejecucion.FechaModificacion = DateTime.UtcNow;
            _context.Entry(ejecucion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EjecucionMensualesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool EjecucionMensualesExists(int id)
        {
            return _context.EjecucionesMensuales.Any(e => e.EjecucionId == id && e.Estado != "N");
        }

        private async Task UpdateActivityResourcesAsync(int obraTareaId)
        {
            // 1. Obtener la Obra/Tarea ACTIVA con su Actividad relacionada
            var obraTarea = await _context.ObrasTareas
                .Include(o => o.Actividad)
                .FirstOrDefaultAsync(o => o.ObraTareaId == obraTareaId && o.Estado == "A");

            // Validación inicial
            if (obraTarea == null || obraTarea.Actividad == null) return;

            // 2. Obtener todas las Obras/Tareas ACTIVAS de la Actividad
            var obraTareasIds = await _context.ObrasTareas
                .Where(o => o.ActividadId == obraTarea.Actividad.ActividadId && o.Estado == "A")
                .Select(o => o.ObraTareaId)
                .ToListAsync();

            // 3. Sumar ONLY ejecuciones ACTIVAS ("A") de las obras/tareas filtradas
            var sumOfMontos = await _context.EjecucionesMensuales
                .Where(e => obraTareasIds.Contains(e.ObraTareaId) && e.Estado == "A")
                .SumAsync(e => e.Monto);

            // 4. Actualizar recursos restantes en la Actividad
            obraTarea.Actividad.RecursosRestantes = obraTarea.Actividad.RecursosActividad - sumOfMontos;

            _context.Entry(obraTarea.Actividad).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
