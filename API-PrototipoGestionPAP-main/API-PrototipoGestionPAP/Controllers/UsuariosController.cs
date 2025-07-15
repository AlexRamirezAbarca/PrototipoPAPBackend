using Microsoft.AspNetCore.Mvc;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : BaseController<Usuarios, UsuariosResponse, UsuariosRequest>
    {
        private readonly IConfiguration _configuration;
        private readonly PersonasController _personasController;

        public UsuariosController(DBContext context, IConfiguration configuration, PersonasController personasController)
            : base(context, "Usuario", "UsuarioId")
        {
            _configuration = configuration;
            _personasController = personasController;
        }

        private (string HashedPassword, ActionResult ErrorResult) HashPassword(string password)
        {
            var secretKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(secretKey))
            {
                return (null, StatusCode(500, new BaseResponse<object>
                {
                    Mensaje = "Error interno: clave secreta no configurada.",
                    Datos = new { }
                }));
            }
            var passwordWithSecret = $"{password}{secretKey}";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(passwordWithSecret);
            return (hashedPassword, null);
        }

        [HttpGet]
        public override async Task<ActionResult> GetAll(int page = 1, int pageSize = 10, string filter = null, string filterField = null,
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

            var query = _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Persona)
                .AsQueryable();

            query = query.Where(e => e.Estado != "N");

            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterField))
            {
                var property = typeof(Usuarios).GetProperty(filterField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property == null)
                {
                    return BadRequest(new BaseResponse<object>
                    {
                        Mensaje = $"No se encontró la propiedad '{filterField}' en Usuarios.",
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

                var parameter = Expression.Parameter(typeof(Usuarios), "x");
                var propertyAccess = Expression.Property(parameter, property);
                var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var filterExpression = Expression.Call(propertyAccess, method, Expression.Constant(filter, typeof(string)));
                var lambda = Expression.Lambda<Func<Usuarios, bool>>(filterExpression, parameter);

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
                .Select(e => new UsuariosResponse
                {
                    UsuarioId = e.UsuarioId,
                    PersonaId = e.PersonaId,
                    RolId = e.RolId,
                    NombreUsuario = e.NombreUsuario,
                    Rol = e.Rol == null
                        ? null
                        : new RolesResponse
                        {
                            RolId = e.Rol.RolId,
                            Nombre = e.Rol.Nombre,
                            Descripcion = e.Rol.Descripcion
                        },
                    Persona = e.Persona == null
                        ? null
                        : new PersonasResponse
                        {
                            PersonaId = e.Persona.PersonaId,
                            Cedula = e.Persona.Cedula,
                            Nombre = e.Persona.Nombre,
                            Apellido = e.Persona.Apellido,
                            CorreoElectronico = e.Persona.CorreoElectronico,
                            Telefono = e.Persona.Telefono
                        }
                })
                .ToList();

            var filterFields = typeof(UsuariosResponse)
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


        protected override bool EntityExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }

        [HttpPost]
        public override async Task<ActionResult> Create(UsuariosRequest request)
        {
            if (request == null)
            {
                return BadRequest(new BaseResponse<object>
                {
                    Mensaje = "El cuerpo de la solicitud no puede ser nulo.",
                    Datos = new { }
                });
            }

            var usuario = UniversalMapper.Map<UsuariosRequest, Usuarios>(request);

            if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario == usuario.NombreUsuario))
            {
                return BadRequest(new BaseResponse<object>
                {
                    Mensaje = "El nombre de usuario ya está en uso.",
                    Datos = new { }
                });
            }

            var (hashedPassword, error) = HashPassword(usuario.Contraseña);
            if (error != null) return error;
            usuario.Contraseña = hashedPassword;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var usuarioResponse = UniversalMapper.Map<Usuarios, UsuariosResponse>(usuario);

            return Ok(new BaseResponse<UsuariosResponse>
            {
                Mensaje = "Usuario registrado con éxito.",
                Datos = usuarioResponse
            });
        }

        [HttpPut("{id}")]
        public override async Task<ActionResult> Update(int id, UsuariosRequest request)
        {
            if (request == null)
            {
                return BadRequest(new BaseResponse<object>
                {
                    Mensaje = "El cuerpo de la solicitud no puede ser nulo.",
                    Datos = new { }
                });
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(new BaseResponse<object>
                {
                    Mensaje = "El usuario no existe.",
                    Datos = new { }
                });
            }
            request.UsuarioId = id;
            var currentPassword = usuario.Contraseña;
            UniversalMapper.Map(request, usuario);

            if (!string.IsNullOrEmpty(request.Contraseña))
            {
                var (hashedPassword, error) = HashPassword(request.Contraseña);
                if (error != null) return error;
                usuario.Contraseña = hashedPassword;
            }
            else
            {
                usuario.Contraseña = currentPassword;
            }

            try
            {
                _context.Entry(usuario).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var usuarioResponse = UniversalMapper.Map<Usuarios, UsuariosResponse>(usuario);

                return Ok(new BaseResponse<UsuariosResponse>
                {
                    Mensaje = "Usuario actualizado con éxito.",
                    Datos = usuarioResponse
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound(new BaseResponse<object>
                    {
                        Mensaje = "El usuario no existe.",
                        Datos = new { }
                    });
                }
                else
                {
                    throw;
                }
            }
        }


        [HttpPost("crear-con-persona")]
        public async Task<ActionResult> CrearConPersona([FromBody] CrearUsuarioConPersonaRequest request)
        {
            if (request == null || request.Usuario == null || request.Persona == null)
            {
                return BadRequest(new BaseResponse<object>
                {
                    Mensaje = "El cuerpo de la solicitud no puede ser nulo y debe contener tanto Usuario como Persona.",
                    Datos = new { }
                });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var personaExistente = await _context.Personas
                    .FirstOrDefaultAsync(p => p.Cedula == request.Persona.Cedula);

                if (personaExistente == null)
                {
                    var personaRequest = request.Persona;
                    var nuevaPersona = UniversalMapper.Map<PersonasRequest, Personas>(personaRequest);

                    _context.Personas.Add(nuevaPersona);
                    await _context.SaveChangesAsync();

                    personaExistente = nuevaPersona;
                }

                request.Usuario.PersonaId = personaExistente.PersonaId;

                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.NombreUsuario == request.Usuario.NombreUsuario);

                if (usuarioExistente != null)
                {
                    if (usuarioExistente.PersonaId == personaExistente.PersonaId)
                    {
                        var usuarioResponseExistente = UniversalMapper.Map<Usuarios, UsuariosResponse>(usuarioExistente);
                        await transaction.CommitAsync();

                        return Ok(new BaseResponse<UsuariosResponse>
                        {
                            Mensaje = "El usuario ya está asignado a esta persona.",
                            Datos = usuarioResponseExistente
                        });
                    }
                    else
                    {
                        usuarioExistente.PersonaId = personaExistente.PersonaId;

                        if (!string.IsNullOrEmpty(request.Usuario.Contraseña))
                        {
                            var (hashedPassword, error) = HashPassword(request.Usuario.Contraseña);
                            if (error != null) return error;
                            usuarioExistente.Contraseña = hashedPassword;
                        }

                        _context.Entry(usuarioExistente).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        var usuarioResponseReasignado = UniversalMapper.Map<Usuarios, UsuariosResponse>(usuarioExistente);
                        await transaction.CommitAsync();

                        return Ok(new BaseResponse<UsuariosResponse>
                        {
                            Mensaje = "Usuario reasignado a la persona existente.",
                            Datos = usuarioResponseReasignado
                        });
                    }
                }

                var (hashedPasswordFinal, errorFinal) = HashPassword(request.Usuario.Contraseña);
                if (errorFinal != null) return errorFinal;
                request.Usuario.Contraseña = hashedPasswordFinal;

                var nuevoUsuario = UniversalMapper.Map<UsuariosRequest, Usuarios>(request.Usuario);
                nuevoUsuario.PersonaId = personaExistente.PersonaId;

                _context.Usuarios.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var nuevoUsuarioResponse = UniversalMapper.Map<Usuarios, UsuariosResponse>(nuevoUsuario);

                return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.UsuarioId }, new BaseResponse<UsuariosResponse>
                {
                    Mensaje = "Usuario y Persona creados o reasignados con éxito.",
                    Datos = nuevoUsuarioResponse
                });
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new BaseResponse<object>
                {
                    Mensaje = "Ocurrió un error al procesar la solicitud.",
                    Datos = new { }
                });
            }
        }
    }
}
