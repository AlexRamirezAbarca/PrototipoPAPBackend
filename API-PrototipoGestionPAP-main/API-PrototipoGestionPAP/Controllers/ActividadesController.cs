using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadesController : BaseController<Actividades, ActividadesResponse, ActividadesRequest>
    {
        public ActividadesController(DBContext context)
            : base(context, "Actividad", "ActividadId")
        {
        }

        [HttpGet]
        public override async Task<ActionResult> GetAll(
           [FromQuery] int page = 1,
           [FromQuery] int pageSize = 10,
           [FromQuery] string filter = null,
           [FromQuery] string filterField = null,
           [FromQuery] int planificacionId = 0
        )
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest(new BaseResponse<object>
                {
                    Mensaje = "Page y PageSize deben ser mayores a 0.",
                    Datos = new { }
                });
            }

            if (planificacionId <= 0)
            {
                return BadRequest(new BaseResponse<object>
                {
                    Mensaje = "El parámetro 'planificacionId' debe ser mayor a 0.",
                    Datos = new { }
                });
            }

            var planificacionExiste = await _context.PlanificacionesAnuales
                .AnyAsync(p => p.PlanificacionId == planificacionId);

            if (!planificacionExiste)
            {
                return NotFound(new BaseResponse<object>
                {
                    Mensaje = $"No se encontró una Planificación con el id {planificacionId}.",
                    Datos = new { }
                });
            }

            IQueryable<Actividades> query = _context.Actividades
                .Where(a => a.Estado != "N" && a.PlanificacionId == planificacionId)
                .Include(a => a.Planificacion)
                .Include(a => a.UnidadResp)
                .Include(a => a.ObjEstr)
                .Include(a => a.UnidadEjecutora)
                .Include(a => a.ObjetivoOperativo)
                .Include(a => a.Carrera)
                .Include(a => a.Facultad)
                .Include(a => a.EjePn)
                .Include(a => a.ObjPn)
                .Include(a => a.PoliticaPn)
                .Include(a => a.MetaPn)
                .Include(a => a.ProgramaNac)
                .Include(a => a.ProgramaInst)
                .Include(a => a.ProductoInst)
                .Include(a => a.Accion)
                .Include(a => a.Indicador)
                .Include(a => a.ProgramaPre);

            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterField))
            {
                PropertyInfo property = typeof(Actividades).GetProperty(filterField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                {
                    return BadRequest(new BaseResponse<object>
                    {
                        Mensaje = $"No se encontró la propiedad '{filterField}' en la entidad Actividades.",
                        Datos = new { }
                    });
                }

                if (property.PropertyType != typeof(string))
                {
                    return BadRequest(new BaseResponse<object>
                    {
                        Mensaje = $"La propiedad '{filterField}' no es de tipo string y no se puede aplicar un filtro de texto.",
                        Datos = new { }
                    });
                }

                var parameter = Expression.Parameter(typeof(Actividades), "x");
                var propertyAccess = Expression.Property(parameter, property);
                var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var filterExpression = Expression.Call(propertyAccess, method, Expression.Constant(filter, typeof(string)));
                var lambda = Expression.Lambda<Func<Actividades, bool>>(filterExpression, parameter);

                query = query.Where(lambda);
            }
            else if (!string.IsNullOrEmpty(filter) || !string.IsNullOrEmpty(filterField))
            {
                return BadRequest(new BaseResponse<object>
                {
                    Mensaje = "Ambos parámetros 'filter' y 'filterField' deben ser proporcionados para aplicar un filtro.",
                    Datos = new { }
                });
            }

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            if (page > totalPages && totalPages > 0)
            {
                return BadRequest(new BaseResponse<object>
                {
                    Mensaje = "La página solicitada excede el total de páginas disponibles.",
                    Datos = new { }
                });
            }

            var entities = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var entitiesResponse = entities.Select(e =>
            {
                var r = UniversalMapper.Map<Actividades, ActividadesNameResponse>(e);

                r.PlanificacionName = e.Planificacion?.Descripcion;   
                r.UnidadRespName = e.UnidadResp?.Nombre;      
                r.ObjEstrName = e.ObjEstr?.Nombre;
                r.UnidadEjecutoraName = e.UnidadEjecutora?.Nombre;
                r.ObjetivoOperativoName = e.ObjetivoOperativo?.Nombre;
                r.CarreraName = e.Carrera?.Nombre;
                r.FacultadName = e.Facultad?.Nombre;
                r.EjePnName = e.EjePn?.Nombre;
                r.ObjPnName = e.ObjPn?.Nombre;
                r.PoliticaPnName = e.PoliticaPn?.Nombre;
                r.MetaPnName = e.MetaPn?.Nombre;
                r.ProgramaNacName = e.ProgramaNac?.Nombre;
                r.ProgramaInstName = e.ProgramaInst?.Nombre;
                r.ProductoInstName = e.ProductoInst?.Nombre;
                r.AccionName = e.Accion?.Nombre;
                r.IndicadorName = e.Indicador?.NombreIndicador;
                r.ProgramaPreName = e.ProgramaPre?.Nombre;

                return r;
            }).ToList();

            var filterFields = typeof(ActividadesResponse)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(string))
                .Select(p => p.Name)
                .ToList();

            var response = new
            {
                Data = entitiesResponse,
                Pagination = new
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    TotalRecords = totalRecords
                },
                FilterFields = filterFields
            };

            return Ok(new BaseResponse<object>
            {
                Mensaje = "Datos obtenidos correctamente.",
                Datos = response
            });
        }

        [HttpPost]
        public override async Task<ActionResult> Create(ActividadesRequest request)
        {
            if (request == null)
            {
                return BadRequest(new BaseResponse<object>
                {
                    Mensaje = "El cuerpo de la solicitud no puede ser nulo.",
                    Datos = new { }
                });
            }

            request.RecursosRestantes = request.RecursosActividad;
            var newEntity = UniversalMapper.Map<ActividadesRequest, Actividades>(request);

            PropertyInfo estadoProperty = typeof(Actividades).GetProperty("Estado", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (estadoProperty != null && estadoProperty.PropertyType == typeof(string))
            {
                estadoProperty.SetValue(newEntity, "A");
            }

            _context.Set<Actividades>().Add(newEntity);
            await _context.SaveChangesAsync();

            PropertyInfo idProperty = typeof(Actividades).GetProperty("ActividadId", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (idProperty == null)
            {
                return CreatedAtAction(nameof(GetById), new { id = 0 }, new BaseResponse<object>
                {
                    Mensaje = "Actividad creada correctamente.",
                    Datos = new { }
                });
            }

            var idValue = idProperty.GetValue(newEntity);

            var newResponse = UniversalMapper.Map<Actividades, ActividadesResponse>(newEntity);

            return CreatedAtAction(nameof(GetById), new { id = idValue }, new BaseResponse<ActividadesResponse>
            {
                Mensaje = "Actividad creada correctamente.",
                Datos = newResponse
            });
        }

        [HttpPut("{id}")]
        public override async Task<ActionResult> Update(int id, ActividadesRequest request)
        {
            if (request == null)
            {
                return BadRequest(new BaseResponse<object>
                {
                    Mensaje = "El cuerpo de la solicitud no puede ser nulo.",
                    Datos = new { }
                });
            }

            var existingEntity = await _context.Actividades.FindAsync(id);
            if (existingEntity == null)
            {
                return NotFound(new BaseResponse<object>
                {
                    Mensaje = "No se encontró la Actividad para actualizar.",
                    Datos = new { }
                });
            }

            PropertyInfo idProperty = typeof(ActividadesRequest).GetProperty("ActividadId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (idProperty != null && idProperty.CanWrite)
            {
                idProperty.SetValue(request, id);
            }

            UniversalMapper.Map(request, existingEntity);

            // Actualización específica de Actividades
            PropertyInfo fechaModificacionProperty = typeof(Actividades).GetProperty("FechaModificacion", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (fechaModificacionProperty != null && fechaModificacionProperty.PropertyType == typeof(DateTime))
            {
                fechaModificacionProperty.SetValue(existingEntity, DateTime.UtcNow);
            }

            try
            {
                await _context.SaveChangesAsync();
                await UpdateActivityResourcesAsync(id);

                var updatedResponse = UniversalMapper.Map<Actividades, ActividadesResponse>(existingEntity);

                return Ok(new BaseResponse<ActividadesResponse>
                {
                    Mensaje = "Actividad actualizada correctamente.",
                    Datos = updatedResponse
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound(new BaseResponse<object>
                    {
                        Mensaje = "No se encontró la Actividad para actualizar.",
                        Datos = new { }
                    });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task UpdateActivityResourcesAsync(int actividadId)
        {
            // 1. Obtener la Actividad ACTIVA
            var actividad = await _context.Actividades
                .FirstOrDefaultAsync(a => a.ActividadId == actividadId && a.Estado == "A");

            if (actividad == null) return;

            // 2. Obtener IDs de Obras/Tareas ACTIVAS de la Actividad
            var obraTareasIds = await _context.ObrasTareas
                .Where(ot => ot.ActividadId == actividadId && ot.Estado == "A")
                .Select(ot => ot.ObraTareaId)
                .ToListAsync();

            // 3. Sumar montos de Ejecuciones ACTIVAS
            var sumOfMontos = await _context.EjecucionesMensuales
                .Where(em => obraTareasIds.Contains(em.ObraTareaId) && em.Estado == "A")
                .SumAsync(em => em.Monto);

            // 4. Actualizar recursos restantes
            actividad.RecursosRestantes = actividad.RecursosActividad - sumOfMontos;

            _context.Entry(actividad).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        protected override bool EntityExists(int id)
        {
            return _context.Actividades.Any(e => e.ActividadId == id && e.Estado != "N");
        }
    }
}
