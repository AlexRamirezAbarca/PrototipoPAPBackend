using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_PrototipoGestionPAP.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanificacionesAnualesController : ControllerBase
    {
        private readonly DBContext _context;

        public PlanificacionesAnualesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/PlanificacionesAnuales
        [HttpGet]
        public async Task<ActionResult> GetPlanificacionesAnuales(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string filter = null,
            [FromQuery] string filterField = null)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and PageSize must be greater than 0.");
            }

            IQueryable<PlanificacionesAnuales> query = _context.PlanificacionesAnuales.Where(x => x.Estado != "N");

            // Aplicar filtrado si se proporcionan los parámetros
            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterField))
            {
                PropertyInfo property = typeof(PlanificacionesAnuales).GetProperty(filterField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                {
                    return BadRequest($"No se encontró la propiedad '{filterField}' en PlanificacionesAnuales.");
                }

                // Determinar el tipo de propiedad para aplicar el filtrado adecuado
                if (property.PropertyType == typeof(string))
                {
                    var parameter = Expression.Parameter(typeof(PlanificacionesAnuales), "x");
                    var propertyAccess = Expression.Property(parameter, property);
                    var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var filterExpression = Expression.Call(propertyAccess, method, Expression.Constant(filter, typeof(string)));
                    var lambda = Expression.Lambda<Func<PlanificacionesAnuales, bool>>(filterExpression, parameter);

                    query = query.Where(lambda);
                }
                else if (property.PropertyType == typeof(int))
                {
                    if (int.TryParse(filter, out int filterValue))
                    {
                        var parameter = Expression.Parameter(typeof(PlanificacionesAnuales), "x");
                        var propertyAccess = Expression.Property(parameter, property);
                        var constant = Expression.Constant(filterValue, typeof(int));
                        var equality = Expression.Equal(propertyAccess, constant);
                        var lambda = Expression.Lambda<Func<PlanificacionesAnuales, bool>>(equality, parameter);

                        query = query.Where(lambda);
                    }
                    else
                    {
                        return BadRequest($"El valor de filtro para la propiedad '{filterField}' debe ser un número entero.");
                    }
                }
                else
                {
                    return BadRequest($"La propiedad '{filterField}' no es compatible para filtrado.");
                }
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

            var planificaciones = await query
                .OrderBy(p => p.Anio) // Ordenar por año, puedes ajustar esto según tus necesidades
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new
            {
                Data = planificaciones,
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

        // GET: api/PlanificacionesAnuales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanificacionesAnuales>> GetPlanificacionesAnuales(int id)
        {
            var planificacionAnual = await _context.PlanificacionesAnuales
                                                    .Include(p => p.Actividades)
                                                    .FirstOrDefaultAsync(p => p.PlanificacionId == id && p.Estado != "N");

            if (planificacionAnual == null)
            {
                return NotFound();
            }

            return planificacionAnual;
        }

        // PUT: api/PlanificacionesAnuales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanificacionesAnuales(int id, PlanificacionesAnuales planificacionesAnuales)
        {
            if (id != planificacionesAnuales.PlanificacionId)
            {
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo de la solicitud.");
            }

            var existingEntity = await _context.PlanificacionesAnuales.FindAsync(id);
            if (existingEntity == null || existingEntity.Estado == "N")
            {
                return NotFound();
            }

            // Verificar si el año está siendo actualizado y si ya existe otro registro con el mismo año
            if (existingEntity.Anio != planificacionesAnuales.Anio)
            {
                bool anioExists = await _context.PlanificacionesAnuales.AnyAsync(p => p.Anio == planificacionesAnuales.Anio && p.Estado != "N");
                if (anioExists)
                {
                    return Conflict($"Ya existe una planificación para el año {planificacionesAnuales.Anio}.");
                }
            }

            existingEntity.Anio = planificacionesAnuales.Anio;
            existingEntity.Descripcion = planificacionesAnuales.Descripcion;
            existingEntity.ModificadoPor = planificacionesAnuales.ModificadoPor;
            existingEntity.FechaModificacion = DateTime.UtcNow;
            existingEntity.Estado = planificacionesAnuales.Estado ?? existingEntity.Estado;

            _context.Entry(existingEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanificacionesAnualesExists(id))
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

        // POST: api/PlanificacionesAnuales
        [HttpPost]
        public async Task<ActionResult<PlanificacionesAnuales>> PostPlanificacionesAnuales(PlanificacionesAnuales planificacionesAnuales)
        {
            // Verificar si ya existe una planificación para el mismo año
            bool anioExists = await _context.PlanificacionesAnuales.AnyAsync(p => p.Anio == planificacionesAnuales.Anio && p.Estado != "N");
            if (anioExists)
            {
                return Conflict($"Ya existe una planificación para el año {planificacionesAnuales.Anio}.");
            }

            planificacionesAnuales.Estado = "A"; // Establecer el estado a Activo por defecto
            planificacionesAnuales.FechaCreacion = DateTime.UtcNow;

            _context.PlanificacionesAnuales.Add(planificacionesAnuales);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlanificacionesAnuales", new { id = planificacionesAnuales.PlanificacionId }, planificacionesAnuales);
        }

        // DELETE: api/PlanificacionesAnuales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanificacionesAnuales(int id)
        {
            var planificacionesAnuales = await _context.PlanificacionesAnuales.FindAsync(id);
            if (planificacionesAnuales == null || planificacionesAnuales.Estado == "N")
            {
                return NotFound();
            }

            // Verificar si hay actividades asociadas antes de realizar el soft delete
            bool hasActividades = await _context.Actividades.AnyAsync(a => a.PlanificacionId == id && a.Estado != "N");
            if (hasActividades)
            {
                return BadRequest("No se puede eliminar la planificación anual porque tiene actividades asociadas.");
            }

            planificacionesAnuales.Estado = "N";
            planificacionesAnuales.FechaModificacion = DateTime.UtcNow;

            _context.Entry(planificacionesAnuales).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanificacionesAnualesExists(id))
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

        private bool PlanificacionesAnualesExists(int id)
        {
            return _context.PlanificacionesAnuales.Any(e => e.PlanificacionId == id && e.Estado != "N");
        }
    }
}
