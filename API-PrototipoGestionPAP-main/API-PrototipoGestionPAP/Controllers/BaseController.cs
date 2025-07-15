using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_PrototipoGestionPAP.Utils;
using API_PrototipoGestionPAP.Models;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity, TResponse, TRequest> : ControllerBase
        where TEntity : class, new()
        where TResponse : class, new()
        where TRequest : class
    {
        protected readonly DBContext _context;
        protected readonly string _entityNamePlural;
        protected readonly string _entityNameSingular;
        protected readonly string _entityIdName;

        protected BaseController(DBContext context, string entityNameSingular, string entityIdName)
        {
            _context = context;
            _entityNamePlural = SplitCamelCase(typeof(TEntity).Name);
            _entityNameSingular = entityNameSingular;
            _entityIdName = entityIdName;
        }

        private string SplitCamelCase(string input)
        {
            return Regex.Replace(input, "(\\B[A-Z])", " $1");
        }

        // GET: api/[controller]
        [HttpGet]
        public virtual async Task<ActionResult> GetAll(
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
                    Mensaje = "Page and PageSize must be greater than 0.",
                    Datos = new { }
                });
            }

            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();

            PropertyInfo estadoProperty = typeof(TEntity).GetProperty("Estado", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (estadoProperty != null && estadoProperty.PropertyType == typeof(string))
            {
                query = query.Where(e => EF.Property<string>(e, "Estado") != "N");
            }

            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterField))
            {
                PropertyInfo property = typeof(TEntity).GetProperty(filterField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                {
                    return BadRequest(new BaseResponse<object>
                    {
                        Mensaje = $"No se encontró la propiedad '{filterField}' en {_entityNamePlural}.",
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

                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var propertyAccess = Expression.Property(parameter, property);
                var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var filterExpression = Expression.Call(propertyAccess, method, Expression.Constant(filter, typeof(string)));
                var lambda = Expression.Lambda<Func<TEntity, bool>>(filterExpression, parameter);

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

            var entitiesResponse = entities
                .Select(e => UniversalMapper.Map<TEntity, TResponse>(e))
                .ToList();

            var filterFields = typeof(TResponse)
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

        // GET: api/[controller]/{id}
        [HttpGet("{id}")]
        public virtual async Task<ActionResult> GetById(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                return NotFound(new BaseResponse<object>
                {
                    Mensaje = $"No se encontró la {_entityNameSingular}.",
                    Datos = new { }
                });
            }

            var entityResponse = UniversalMapper.Map<TEntity, TResponse>(entity);

            return Ok(new BaseResponse<TResponse>
            {
                Mensaje = $"{_entityNameSingular} obtenida correctamente.",
                Datos = entityResponse
            });
        }

        // PUT: api/[controller]/{id}
        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Update(int id, TRequest request)
        {
            if (request == null)
            {
                return BadRequest(new BaseResponse<object>
                {
                    Mensaje = "El cuerpo de la solicitud no puede ser nulo.",
                    Datos = new { }
                });
            }

            var existingEntity = await _context.Set<TEntity>().FindAsync(id);
            if (existingEntity == null)
            {
                return NotFound(new BaseResponse<object>
                {
                    Mensaje = $"No se encontró la {_entityNameSingular} para actualizar.",
                    Datos = new { }
                });
            }

            PropertyInfo idProperty = typeof(TRequest).GetProperty(_entityIdName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (idProperty != null && idProperty.CanWrite)
            {
                idProperty.SetValue(request, id);
            }

            UniversalMapper.Map(request, existingEntity);

            PropertyInfo fechaModificacionProperty = typeof(TEntity).GetProperty("FechaModificacion", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (fechaModificacionProperty != null && fechaModificacionProperty.PropertyType == typeof(DateTime))
            {
                fechaModificacionProperty.SetValue(existingEntity, DateTime.UtcNow);
            }

            try
            {
                await _context.SaveChangesAsync();

                var updatedResponse = UniversalMapper.Map<TEntity, TResponse>(existingEntity);

                return Ok(new BaseResponse<TResponse>
                {
                    Mensaje = $"{_entityNameSingular} actualizada correctamente.",
                    Datos = updatedResponse
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound(new BaseResponse<object>
                    {
                        Mensaje = $"No se encontró la {_entityNameSingular} para actualizar.",
                        Datos = new { }
                    });
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/[controller]
        [HttpPost]
        public virtual async Task<ActionResult> Create(TRequest request)
        {
            if (request == null)
            {
                return BadRequest(new BaseResponse<object>
                {
                    Mensaje = "El cuerpo de la solicitud no puede ser nulo.",
                    Datos = new { }
                });
            }

            var newEntity = UniversalMapper.Map<TRequest, TEntity>(request);

            PropertyInfo estadoProperty = typeof(TEntity).GetProperty("Estado", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (estadoProperty != null && estadoProperty.PropertyType == typeof(string))
            {
                estadoProperty.SetValue(newEntity, "A");
            }

            _context.Set<TEntity>().Add(newEntity);
            await _context.SaveChangesAsync();

            PropertyInfo idProperty = typeof(TEntity).GetProperty(_entityIdName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (idProperty == null)
            {
                return CreatedAtAction(nameof(GetById), new { id = 0 }, new BaseResponse<object>
                {
                    Mensaje = $"{_entityNameSingular} creada correctamente.",
                    Datos = new { }
                });
            }

            var idValue = idProperty.GetValue(newEntity);

            var newResponse = UniversalMapper.Map<TEntity, TResponse>(newEntity);

            return CreatedAtAction(nameof(GetById), new { id = idValue }, new BaseResponse<TResponse>
            {
                Mensaje = $"{_entityNameSingular} creada correctamente.",
                Datos = newResponse
            });
        }

        // DELETE: api/[controller]/{id}
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return NotFound(new BaseResponse<object>
                {
                    Mensaje = $"No se encontró la {_entityNameSingular} para eliminar.",
                    Datos = new { }
                });
            }

            PropertyInfo estadoProperty = typeof(TEntity).GetProperty("Estado", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (estadoProperty != null && estadoProperty.PropertyType == typeof(string))
            {
                estadoProperty.SetValue(entity, "N");
            }

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(new BaseResponse<object>
                {
                    Mensaje = $"{_entityNameSingular} eliminada correctamente.",
                    Datos = new { }
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound(new BaseResponse<object>
                    {
                        Mensaje = $"No se encontró la {_entityNameSingular} para eliminar.",
                        Datos = new { }
                    });
                }
                else
                {
                    throw;
                }
            }
        }

        protected abstract bool EntityExists(int id);
    }
}
