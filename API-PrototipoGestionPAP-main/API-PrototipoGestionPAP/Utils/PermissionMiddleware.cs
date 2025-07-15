using System;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using API_PrototipoGestionPAP.Models;

namespace API_PrototipoGestionPAP.Utils
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, DBContext dbContext, IConfiguration configuration)
        {
            // Verifica si el endpoint actual requiere validación de permisos
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var permissionAttribute = endpoint.Metadata.GetMetadata<PermissionAttribute>();
                if (permissionAttribute != null)
                {
                    // Extrae el token JWT del header Authorization
                    string authHeader = context.Request.Headers["Authorization"];
                    if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("No se proporcionó un token válido.");
                        return;
                    }

                    string token = authHeader.Substring("Bearer ".Length).Trim();

                    // Valida el token y extrae los claims
                    var tokenHandler = new JwtSecurityTokenHandler();
                    JwtSecurityToken jwtToken = null;
                    try
                    {
                        jwtToken = tokenHandler.ReadJwtToken(token);
                    }
                    catch (Exception)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Token inválido.");
                        return;
                    }

                    // Se espera que el token incluya un claim "rolId"
                    var rolClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "rolId");
                    if (rolClaim == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("El token no contiene información de rol.");
                        return;
                    }

                    if (!int.TryParse(rolClaim.Value, out int rolId))
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("El rol en el token no es válido.");
                        return;
                    }

                    // Consultar la base de datos para obtener el rol y sus permisos (incluyendo la entidad Permisos)
                    var rol = await dbContext.Roles
                        .Include(r => r.RolPermisos)
                            .ThenInclude(rp => rp.Permiso)
                        .FirstOrDefaultAsync(r => r.RolId == rolId);

                    if (rol == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("Rol no encontrado.");
                        return;
                    }

                    // Valida si el rol tiene asignado el permiso requerido
                    bool tienePermiso = rol.RolPermisos
                        .Any(rp => rp.Permiso != null &&
                                   rp.Permiso.Codigo.Equals(permissionAttribute.PermissionCode, StringComparison.OrdinalIgnoreCase));

                    if (!tienePermiso)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("No tiene permisos para acceder a este recurso.");
                        return;
                    }
                }
            }

            // Continúa con la siguiente parte del pipeline
            await _next(context);
        }
    }
}
