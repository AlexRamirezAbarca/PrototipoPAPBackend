using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Inbound;
using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound;
using API_PrototipoGestionPAP.Models.Mantenedores;
using Microsoft.EntityFrameworkCore;

namespace API_PrototipoGestionPAP.Services.Mantenedores
{
    public class ObjetivoMetaPNService
    {
        private readonly DBContext _context;

        public ObjetivoMetaPNService(DBContext context)
        {
            _context = context;
        }

        public async Task<List<ObjetivoMetaResponseDto>> GetAllAsync()
        {
            return await _context.ObjetivosMetasPN
                .Where(x => x.Estado == "A")
                .Include(x => x.Objetivo)
                .Include(x => x.Meta)
                .Select(x => new ObjetivoMetaResponseDto
                {
                    ObjetivoMetaPnId = x.ObjetivoMetaPnId,

                    ObjPnId = x.ObjPnId,
                    ObjPnNombre = x.Objetivo != null ? x.Objetivo.Nombre : string.Empty,

                    MetaPnId = x.MetaPnId,
                    MetaPnNombre = x.Meta != null ? x.Meta.Nombre : string.Empty,

                    Estado = x.Estado,
                    FechaCreacion = x.FechaCreacion
                })
                .ToListAsync();
        }

        public async Task<string> CreateAsync(ObjetivoMetaRequestDto dto)
        {
            var exists = await _context.ObjetivosMetasPN
                .AnyAsync(x => x.ObjPnId == dto.ObjPnId && x.MetaPnId == dto.MetaPnId && x.Estado == "A");

            if (exists)
                return "Esta relación ya existe.";

            var entity = new ObjetivoMetaPN
            {
                ObjPnId = dto.ObjPnId,
                MetaPnId = dto.MetaPnId,
                CreadoPor = dto.CreadoPor
            };

            _context.ObjetivosMetasPN.Add(entity);
            await _context.SaveChangesAsync();

            return "Relación creada correctamente.";
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ObjetivosMetasPN.FindAsync(id);
            if (entity == null || entity.Estado != "A")
                return false;

            entity.Estado = "I";
            entity.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ObjetivoMetaResponseDto>> GetByObjetivoIdAsync(int objPnId)
        {
            return await _context.ObjetivosMetasPN
                .Where(x => x.ObjPnId == objPnId && x.Estado == "A")
                .Include(x => x.Objetivo)
                .Include(x => x.Meta)
                .Select(x => new ObjetivoMetaResponseDto
                {
                    ObjetivoMetaPnId = x.ObjetivoMetaPnId,

                    ObjPnId = x.ObjPnId,
                    ObjPnNombre = x.Objetivo != null ? x.Objetivo.Nombre : string.Empty,

                    MetaPnId = x.MetaPnId,
                    MetaPnNombre = x.Meta != null ? x.Meta.Nombre : string.Empty,

                    Estado = x.Estado,
                    FechaCreacion = x.FechaCreacion
                })
                .ToListAsync();
        }
    }
}
