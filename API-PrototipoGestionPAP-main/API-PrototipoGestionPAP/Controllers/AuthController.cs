using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using UAParser;

namespace API_PrototipoGestionPAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UsuariosController _usuariosController;
        private readonly DBContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(UsuariosController usuariosController, DBContext context, IConfiguration configuration)
        {
            _usuariosController = usuariosController;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuariosRequest request)
        {
            return await _usuariosController.Create(request);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO usuarioLogin)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            var userAgent = Request.Headers["User-Agent"].ToString();

            var parser = Parser.GetDefault();
            var clientInfo = parser.Parse(userAgent);

            string dispositivo = $"{clientInfo.Device.Family} {clientInfo.Device.Brand} {clientInfo.Device.Model}".Trim();
            string navegador = $"{clientInfo.UA.Family} {clientInfo.UA.Major}";

            var user = await _context.Usuarios
                .Include(u => u.Rol)
                    .ThenInclude(r => r.RolPermisos)
                        .ThenInclude(rp => rp.Permiso)
                .FirstOrDefaultAsync(u => u.NombreUsuario == usuarioLogin.NombreUsuario);

            if (user == null)
            {
                return Unauthorized(new BaseResponse<object>
                {
                    Mensaje = "Usuario o contraseña incorrectos.",
                    Datos = new { }
                });
            }


            var sesion = new RegistroSesiones
            {
                UsuarioId = user?.UsuarioId ?? 0,
                FechaInicio = DateTime.UtcNow,
                DireccionIp = ipAddress,
                Dispositivo = string.IsNullOrWhiteSpace(dispositivo) ? "Desconocido" : dispositivo,
                Navegador = string.IsNullOrWhiteSpace(navegador) ? "Desconocido" : navegador
            };

            if (user == null)
            {
                sesion.ResultadoSesion = "Fallido";
                sesion.MensajeError = "Usuario no encontrado.";
                _context.RegistroSesiones.Add(sesion);
                await _context.SaveChangesAsync();
                return Unauthorized(new BaseResponse<object>
                {
                    Mensaje = "Usuario o contraseña incorrectos.",
                    Datos = new { }
                });
            }

            var secretKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(secretKey))
            {
                sesion.ResultadoSesion = "Fallido";
                sesion.MensajeError = "Error interno: clave secreta no configurada.";
                _context.RegistroSesiones.Add(sesion);
                await _context.SaveChangesAsync();
                return StatusCode(500, new BaseResponse<object>
                {
                    Mensaje = "Error interno: clave secreta no configurada.",
                    Datos = new { }
                });
            }

            var passwordWithSecret = $"{usuarioLogin.Contraseña}{secretKey}";
            if (!BCrypt.Net.BCrypt.Verify(passwordWithSecret, user.Contraseña))
            {
                sesion.ResultadoSesion = "Fallido";
                sesion.MensajeError = "Contraseña incorrecta.";
                _context.RegistroSesiones.Add(sesion);
                await _context.SaveChangesAsync();
                return Unauthorized(new BaseResponse<object>
                {
                    Mensaje = "Usuario o contraseña incorrectos.",
                    Datos = new { }
                });
            }

            var token = GenerateJwtToken(user);
            var permisos = user.Rol?.RolPermisos?
                .Where(rp => rp.Estado == "A" && rp.Permiso.Estado == "A")
                .Select(rp => rp.Permiso)
                .ToList();

            var permisosAgrupados = AgruparPermisos(permisos);

            sesion.ResultadoSesion = "Exitoso";
            sesion.MensajeError = null;

            _context.RegistroSesiones.Add(sesion);
            await _context.SaveChangesAsync();

            return Ok(new BaseResponse<object>
            {
                Mensaje = "Inicio de sesión exitoso.",
                Datos = new
                {
                    Token = token,
                    Permisos = permisosAgrupados
                }
            });
        }

        private object AgruparPermisos(List<Permisos>? permisos)
        {
            if (permisos == null)
                return new { };

            var agrupados = permisos.GroupBy(p =>
            {
                var partes = p.Codigo.Split('|');
                return partes[0];
            })
            .ToDictionary(
                grupo => grupo.Key,
                grupo => grupo.Select(p =>
                {
                    var partes = p.Codigo.Split('|');
                    return partes.Length > 1 ? partes[1] : "";
                }).ToList()
            );

            return agrupados;
        }

        private string GenerateJwtToken(Usuarios usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.NombreUsuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("usuarioId", usuario.UsuarioId.ToString()),
                new Claim("rolId", usuario.RolId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
