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
    public class ObrasTareasController : ControllerBase
    {
        private readonly DBContext _context;

        public ObrasTareasController(DBContext context)
        {
            _context = context;
        }

        // GET: api/ObrasTareas
        [HttpGet]
        public async Task<ActionResult> GetObrasTareas(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string filter = null,
            [FromQuery] string filterField = null,
            [FromQuery] int planificacionId = 0)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page y PageSize deben ser mayores a 0.");
            }

            if (planificacionId <= 0)
            {
                return BadRequest("El parámetro 'planificacionId' debe ser mayor a 0.");
            }

            // Verificar si la Planificacion existe
            var planificacionExiste = await _context.PlanificacionesAnuales
                .AnyAsync(p => p.PlanificacionId == planificacionId);

            if (!planificacionExiste)
            {
                return NotFound($"No se encontró una Planificacion con el id {planificacionId}.");
            }

            IQueryable<ObrasTareas> query = _context.ObrasTareas
                .Where(x => x.Estado != "N" && x.Actividad.PlanificacionId == planificacionId)
                .Include(x => x.Actividad)
                .Include(x => x.EjecucionesMensuales.Where(em => em.Estado != "N"));


            // Aplicar filtrado si se proporcionan ambos parámetros
            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterField))
            {
                PropertyInfo property = typeof(ObrasTareas).GetProperty(filterField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                {
                    return BadRequest($"No se encontró la propiedad '{filterField}' en ObrasTareas.");
                }

                if (property.PropertyType != typeof(string))
                {
                    return BadRequest($"La propiedad '{filterField}' no es de tipo string y no se puede aplicar un filtro de texto.");
                }

                var parameter = Expression.Parameter(typeof(ObrasTareas), "x");
                var propertyAccess = Expression.Property(parameter, property);
                var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var filterExpression = Expression.Call(propertyAccess, method, Expression.Constant(filter, typeof(string)));
                var lambda = Expression.Lambda<Func<ObrasTareas, bool>>(filterExpression, parameter);

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

            var obrasTareas = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new
            {
                Data = obrasTareas,
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

        // GET: api/ObrasTareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ObrasTareas>> GetObrasTareas(int id)
        {
            var obraTarea = await _context.ObrasTareas
                .Include(x => x.Actividad)
                .Include(x => x.EjecucionesMensuales)
                .FirstOrDefaultAsync(x => x.ObraTareaId == id);

            if (obraTarea == null || obraTarea.Estado == "N")
            {
                return NotFound();
            }

            return obraTarea;
        }

        // PUT: api/ObrasTareas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutObrasTareas(int id, ObrasTareasRequest request)
        {
            // Buscar la entidad existente
            var existingEntity = await _context.ObrasTareas.FindAsync(id);
            if (existingEntity == null || existingEntity.Estado == "N")
            {
                return NotFound();
            }

            // Opcional: Verificar que el id del request coincida con el parámetro (si el DTO posee la propiedad)
            if (request.ObraTareaId != id)
            {
                return BadRequest("El identificador de la obra/tarea no coincide.");
            }

            // Mapear de forma universal el DTO a la entidad existente
            UniversalMapper.Map(request, existingEntity);

            // Actualizar propiedades internas
            existingEntity.FechaModificacion = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObrasTareasExists(id))
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

        // POST: api/ObrasTareas
        [HttpPost]
        public async Task<ActionResult<ObrasTareas>> PostObrasTareas(ObrasTareasRequest request)
        {
            // Mapear de forma universal el DTO a la entidad ObrasTareas
            var obraTarea = UniversalMapper.Map<ObrasTareasRequest, ObrasTareas>(request);

            // Configurar propiedades adicionales
            obraTarea.Estado = "A"; // Por ejemplo, 'A' para activo
            obraTarea.FechaCreacion = DateTime.UtcNow;

            _context.ObrasTareas.Add(obraTarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetObrasTareas), new { id = obraTarea.ObraTareaId }, obraTarea);
        }

        // DELETE: api/ObrasTareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObrasTareas(int id)
        {
            var obraTarea = await _context.ObrasTareas.FindAsync(id);
            if (obraTarea == null || obraTarea.Estado == "N")
            {
                return NotFound();
            }

            // Marcar la entidad como eliminada lógicamente
            obraTarea.Estado = "N";
            obraTarea.FechaModificacion = DateTime.UtcNow;
            _context.Entry(obraTarea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObrasTareasExists(id))
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

        private bool ObrasTareasExists(int id)
        {
            return _context.ObrasTareas.Any(e => e.ObraTareaId == id && e.Estado != "N");
        }
    }
}
