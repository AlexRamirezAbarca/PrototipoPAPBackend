using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Inbound;
using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound;
using API_PrototipoGestionPAP.Models.Mantenedores;
using Microsoft.EntityFrameworkCore;

namespace API_PrototipoGestionPAP.Services.Mantenedores
{
    public class ObjetivoPoliticaPNService
    {
        private readonly DBContext _context;

        public ObjetivoPoliticaPNService(DBContext context)
        {
            _context = context;
        }

        public async Task<List<ObjetivoPoliticaResponseDto>> GetAllAsync()
        {
            return await _context.ObjetivosPoliticasPN
                .Where(x => x.Estado == "A")
                .Select(x => new ObjetivoPoliticaResponseDto
                {
                    ObjetivoPoliticaPnId = x.ObjetivoPoliticaPnId,
                    ObjPnId = x.ObjPnId,
                    PoliticaPnId = x.PoliticaPnId,
                    Estado = x.Estado,
                    FechaCreacion = x.FechaCreacion
                })
                .ToListAsync();
        }

        public async Task<string> CreateAsync(ObjetivoPoliticaRequestDto dto)
        {
            var exists = await _context.ObjetivosPoliticasPN
                .AnyAsync(x => x.ObjPnId == dto.ObjPnId && x.PoliticaPnId == dto.PoliticaPnId && x.Estado == "A");

            if (exists)
                return "Esta relación ya existe.";

            var entity = new ObjetivoPoliticaPN
            {
                ObjPnId = dto.ObjPnId,
                PoliticaPnId = dto.PoliticaPnId,
                CreadoPor = dto.CreadoPor
            };

            _context.ObjetivosPoliticasPN.Add(entity);
            await _context.SaveChangesAsync();

            return "Relación creada correctamente.";
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ObjetivosPoliticasPN.FindAsync(id);
            if (entity == null || entity.Estado != "A")
                return false;

            entity.Estado = "I";
            entity.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ObjetivoPoliticaResponseDto>> GetByObjetivoIdAsync(int objPnId)
        {
            return await _context.ObjetivosPoliticasPN
                .Where(x => x.ObjPnId == objPnId && x.Estado == "A")
                .Select(x => new ObjetivoPoliticaResponseDto
                {
                    ObjetivoPoliticaPnId = x.ObjetivoPoliticaPnId,
                    ObjPnId = x.ObjPnId,
                    PoliticaPnId = x.PoliticaPnId,
                    Estado = x.Estado,
                    FechaCreacion = x.FechaCreacion
                })
                .ToListAsync();
        }
    }
}
