using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;
using System.Linq.Expressions;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<Roles, RolesResponse, RolesPermisosRequest>
    {
        public RolesController(DBContext context)
            : base(context, "Rol", "RolId")
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
                    Mensaje = "Page and PageSize must be greater than 0.",
                    Datos = new { }
                });
            }

            var query = _context.Roles
                .Include(r => r.RolPermisos)
                .ThenInclude(rp => rp.Permiso)
                .Where(r => r.Estado != "N");

            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterField))
            {
                var property = typeof(Roles).GetProperty(filterField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property == null)
                {
                    return BadRequest(new BaseResponse<object>
                    {
                        Mensaje = $"No se encontró la propiedad '{filterField}' en Roles.",
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

                var parameter = Expression.Parameter(typeof(Roles), "x");
                var propertyAccess = Expression.Property(parameter, property);
                var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var filterExpression = Expression.Call(propertyAccess, method, Expression.Constant(filter, typeof(string)));
                var lambda = Expression.Lambda<Func<Roles, bool>>(filterExpression, parameter);

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

            var roles = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var rolesResponse = new List<RolesResponse>();

            foreach (var role in roles)
            {
                var mappedRole = UniversalMapper.Map<Roles, RolesResponse>(role);
                mappedRole.Permisos = role.RolPermisos
                    .Select(rp => UniversalMapper.Map<Permisos, PermisosResponse>(rp.Permiso))
                    .ToList();
                rolesResponse.Add(mappedRole);
            }

            var filterFields = typeof(RolesResponse)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(string))
                .Select(p => p.Name)
                .ToList();

            var response = new
            {
                Data = rolesResponse,
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
        public override async Task<ActionResult> Create([FromBody] RolesPermisosRequest request)
        {
            if (request?.Rol == null)
            {
                return BadRequest(new BaseResponse<object>
                {
                    Mensaje = "Datos del rol son requeridos.",
                    Datos = new { }
                });
            }

            var rol = new Roles
            {
                Nombre = request.Rol.Nombre,
                Descripcion = request.Rol.Descripcion,
                Estado = "A",
            };

            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();

            foreach (var permiso in request.Permisos)
            {
                var permisoExistente = await _context.Permisos.FirstOrDefaultAsync(p => p.Codigo == permiso.Codigo);
                if (permisoExistente == null)
                {
                    _context.Permisos.Add(permiso);
                    await _context.SaveChangesAsync();
                    permisoExistente = permiso;
                }

                var asociacionExiste = await _context.RolPermisos
                    .AnyAsync(rp => rp.RolId == rol.RolId && rp.PermisoId == permisoExistente.PermisoId);
                if (!asociacionExiste)
                {
                    var rolPermiso = new RolPermisos
                    {
                        RolId = rol.RolId,
                        PermisoId = permisoExistente.PermisoId
                    };
                    _context.RolPermisos.Add(rolPermiso);
                }
            }

            await _context.SaveChangesAsync();

            var rolResponse = UniversalMapper.Map<Roles, RolesResponse>(rol);

            return CreatedAtAction(nameof(GetById), new { id = rol.RolId }, new BaseResponse<RolesResponse>
            {
                Mensaje = "Rol creado exitosamente.",
                Datos = rolResponse
            });
        }

        [HttpPut("{id}")]
        public override async Task<ActionResult> Update(int id, [FromBody] RolesPermisosRequest request)
        {
            if (request?.Rol == null)
            {
                return BadRequest(new BaseResponse<object>
                {
                    Mensaje = "Datos del rol son requeridos.",
                    Datos = new { }
                });
            }

            var rol = await _context.Roles
                .Include(r => r.RolPermisos)
                .FirstOrDefaultAsync(r => r.RolId == id && r.Estado != "N");

            if (rol == null)
            {
                return NotFound(new BaseResponse<object>
                {
                    Mensaje = $"Rol con id {id} no encontrado.",
                    Datos = new { }
                });
            }

            rol.Nombre = request.Rol.Nombre;
            rol.Descripcion = request.Rol.Descripcion;

            var permisosRolActuales = rol.RolPermisos.Select(rp => rp.Permiso).ToList();

            foreach (var permiso in request.Permisos)
            {
                var permisoExistente = await _context.Permisos.FirstOrDefaultAsync(p => p.Codigo == permiso.Codigo);
                if (permisoExistente == null)
                {
                    _context.Permisos.Add(permiso);
                    await _context.SaveChangesAsync();
                    permisoExistente = permiso;
                }

                var asociacionExiste = await _context.RolPermisos
                    .AnyAsync(rp => rp.RolId == rol.RolId && rp.PermisoId == permisoExistente.PermisoId);
                if (!asociacionExiste)
                {
                    var rolPermiso = new RolPermisos
                    {
                        RolId = rol.RolId,
                        PermisoId = permisoExistente.PermisoId
                    };
                    _context.RolPermisos.Add(rolPermiso);
                }
            }

            var codigosPermisosMantener = request.Permisos.Select(p => p.Codigo).ToList();
            var asociacionesParaEliminar = rol.RolPermisos
                .Where(rp => !_context.Permisos.Any(p => p.PermisoId == rp.PermisoId && codigosPermisosMantener.Contains(p.Codigo)))
                .ToList();

            if (asociacionesParaEliminar.Any())
            {
                _context.RolPermisos.RemoveRange(asociacionesParaEliminar);
            }

            await _context.SaveChangesAsync();

            var rolResponse = UniversalMapper.Map<Roles, RolesResponse>(rol);

            return Ok(new BaseResponse<RolesResponse>
            {
                Mensaje = "Rol actualizado exitosamente.",
                Datos = rolResponse
            });
        }
        protected override bool EntityExists(int id)
        {
            return _context.Roles.Any(e => e.RolId == id && e.Estado != "N");
        }
    }
}
