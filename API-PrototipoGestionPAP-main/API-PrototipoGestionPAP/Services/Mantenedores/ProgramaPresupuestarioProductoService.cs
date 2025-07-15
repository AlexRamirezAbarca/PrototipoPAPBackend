using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Inbound;
using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound;
using API_PrototipoGestionPAP.Models.Mantenedores;
using Microsoft.EntityFrameworkCore;

namespace API_PrototipoGestionPAP.Services.Mantenedores
{
    public class ProgramaPresupuestarioProductoService
    {
        private readonly DBContext _context;

        public ProgramaPresupuestarioProductoService(DBContext context)
        {
            _context = context;
        }

        public async Task<List<ProgramaPresupuestarioProductoResponseDto>> GetAllAsync()
        {
            return await _context.ProgramasPresupuestariosProductos
                .Include(x => x.ProgramaPresupuestario)
                .Include(x => x.ProductoInstitucional)
                .Select(x => new ProgramaPresupuestarioProductoResponseDto
                {
                    Id = x.ProgPreProdId,
                    ProgramaPreId = x.ProgramaPreId,
                    ProgramaPreNombre = x.ProgramaPresupuestario.Nombre,
                    ProductoInstId = x.ProductoInstId,
                    ProductoInstNombre = x.ProductoInstitucional.Nombre,
                    Estado = x.Estado,
                    FechaCreacion = x.FechaCreacion
                })
                .ToListAsync();
        }

        public async Task<List<ProgramaPresupuestarioProductoResponseDto>> GetByProgramaPreIdAsync(int programaPreId)
        {
            return await _context.ProgramasPresupuestariosProductos
                .Where(x => x.ProgramaPreId == programaPreId)
                .Include(x => x.ProgramaPresupuestario)
                .Include(x => x.ProductoInstitucional)
                .Select(x => new ProgramaPresupuestarioProductoResponseDto
                {
                    Id = x.ProgPreProdId,
                    ProgramaPreId = x.ProgramaPreId,
                    ProgramaPreNombre = x.ProgramaPresupuestario.Nombre,
                    ProductoInstId = x.ProductoInstId,
                    ProductoInstNombre = x.ProductoInstitucional.Nombre,
                    Estado = x.Estado,
                    FechaCreacion = x.FechaCreacion
                })
                .ToListAsync();
        }

        public async Task CreateAsync(ProgramaPresupuestarioProductoRequestDto dto)
        {
            var entity = new ProgramaPresupuestarioProducto
            {
                ProgramaPreId = dto.ProgramaPreId,
                ProductoInstId = dto.ProductoInstId,
                Estado = "A",
                FechaCreacion = DateTime.Now
            };

            _context.ProgramasPresupuestariosProductos.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ProgramasPresupuestariosProductos.FindAsync(id);
            if (entity != null)
            {
                _context.ProgramasPresupuestariosProductos.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
