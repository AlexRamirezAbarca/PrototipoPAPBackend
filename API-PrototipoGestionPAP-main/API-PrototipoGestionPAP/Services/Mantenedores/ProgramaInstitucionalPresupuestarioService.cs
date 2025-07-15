using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Inbound;
using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound;
using API_PrototipoGestionPAP.Models.Mantenedores;
using Microsoft.EntityFrameworkCore;

namespace API_PrototipoGestionPAP.Services.Mantenedores
{
    public class ProgramaInstitucionalPresupuestarioService
    {
        private readonly DBContext _context;

        public ProgramaInstitucionalPresupuestarioService(DBContext context)
        {
            _context = context;
        }

        public async Task<List<ProgramaInstitucionalPresupuestarioResponseDto>> GetAllAsync()
        {
            return await _context.ProgramasInstitucionalesPresupuestarios
                .Where(x => x.Estado == "A")
                .Include(x => x.ProgramaInstitucional)
                .Include(x => x.ProgramaPresupuestario)
                .Select(x => new ProgramaInstitucionalPresupuestarioResponseDto
                {
                    Id = x.ProgramaInstitucionalPresupuestarioId,
                    ProgramaInstId = x.ProgramaInstId,
                    ProgramaInstNombre = x.ProgramaInstitucional.Nombre,
                    ProgramaPreId = x.ProgramaPreId,
                    ProgramaPreNombre = x.ProgramaPresupuestario.Nombre,
                    Estado = x.Estado,
                    FechaCreacion = x.FechaCreacion
                })
                .ToListAsync();
        }


        public async Task CrearAsync(ProgramaInstitucionalPresupuestarioRequestDto dto)
        {
            var yaExiste = await _context.ProgramasInstitucionalesPresupuestarios.AnyAsync(x =>
                x.ProgramaInstId == dto.ProgramaInstId &&
                x.ProgramaPreId == dto.ProgramaPreId &&
                x.Estado == "A");

            if (yaExiste)
                throw new Exception("Ya existe esta relación.");

            var entity = new ProgramaInstitucionalPresupuestario
            {
                ProgramaInstId = dto.ProgramaInstId,
                ProgramaPreId = dto.ProgramaPreId,
                Estado = "A",
                FechaCreacion = DateTime.Now
            };

            _context.ProgramasInstitucionalesPresupuestarios.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entity = await _context.ProgramasInstitucionalesPresupuestarios.FindAsync(id);
            if (entity == null)
                throw new Exception("No encontrado.");

            entity.Estado = "I";
            entity.FechaModificacion = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task<ProgramaInstitucionalPresupuestarioResponseDto?> GetByIdAsync(int id)
        {
            return await _context.ProgramasInstitucionalesPresupuestarios
                .Where(x => x.ProgramaInstitucionalPresupuestarioId == id)
                .Include(x => x.ProgramaInstitucional)
                .Include(x => x.ProgramaPresupuestario)
                .Select(x => new ProgramaInstitucionalPresupuestarioResponseDto
                {
                    Id = x.ProgramaInstitucionalPresupuestarioId,
                    ProgramaInstId = x.ProgramaInstId,
                    ProgramaInstNombre = x.ProgramaInstitucional.Nombre,
                    ProgramaPreId = x.ProgramaPreId,
                    ProgramaPreNombre = x.ProgramaPresupuestario.Nombre,
                    Estado = x.Estado,
                    FechaCreacion = x.FechaCreacion
                })
                .FirstOrDefaultAsync();
        }


        public async Task<ProgramaInstitucionalJerarquicoResponseDto?> GetDetalleJerarquicoPorProgramaInstitucionalIdAsync(int programaInstId)
        {
            var programa = await _context.ProgramasInstitucionales
                .Where(p => p.ProgramaInstId == programaInstId && p.Estado == "A")
                .Select(p => new ProgramaInstitucionalJerarquicoResponseDto
                {
                    ProgramaInstId = p.ProgramaInstId,
                    Nombre = p.Nombre,

                    ProgramasPresupuestarios = _context.ProgramasInstitucionalesPresupuestarios
                        .Where(pp => pp.ProgramaInstId == p.ProgramaInstId && pp.Estado == "A")
                        .Select(pp => pp.ProgramaPreId)
                        .Distinct()
                        .Select(preId => new ProgramaPresupuestarioJerarquicoDto
                        {
                            ProgramaPreId = preId,
                            Nombre = _context.ProgramasPresupuestarios
                                .Where(pre => pre.ProgramaPreId == preId)
                                .Select(pre => pre.Nombre)
                                .FirstOrDefault() ?? string.Empty,

                            Productos = _context.ProgramasPresupuestariosProductos
                                .Where(rel => rel.ProgramaPreId == preId && rel.Estado == "A")
                                .Select(rel => rel.ProductoInstitucional)
                                .Where(prod => prod.Estado == "A")
                                .Select(prod => new ProductoInstitucionalDto
                                {
                                    ProductoInstId = prod.ProductoInstId,
                                    Nombre = prod.Nombre
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return programa;
        }

    }
}
