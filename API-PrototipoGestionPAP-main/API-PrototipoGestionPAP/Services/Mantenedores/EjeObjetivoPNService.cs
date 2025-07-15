using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Inbound;
using API_PrototipoGestionPAP.Models.DTOs.Mantenedores.Outbound;
using API_PrototipoGestionPAP.Models.Mantenedores;
using Microsoft.EntityFrameworkCore;
namespace API_PrototipoGestionPAP.Services.Mantenedores
{
    public class EjeObjetivoPNService
    {
        private readonly DBContext _context;

        public EjeObjetivoPNService(DBContext context)
        {
            _context = context;
        }

        public async Task<List<EjeObjetivoResponseDto>> GetAllAsync()
        {
            return await _context.EjesObjetivosPN
                .Where(x => x.Estado == "A")
                .Include(x => x.Eje)
                .Include(x => x.Objetivo)
                .Select(x => new EjeObjetivoResponseDto
                {
                    EjeObjetivoPnId = x.EjeObjetivoPnId,

                    EjePnId = x.EjePnId,
                    EjeNombre = x.Eje.Nombre,

                    ObjPnId = x.ObjPnId,
                    ObjetivoNombre = x.Objetivo.Nombre,

                    Estado = x.Estado,
                    FechaCreacion = x.FechaCreacion
                })
                .ToListAsync();
        }


        public async Task<string> CreateAsync(EjeObjetivoRequestDto dto)
        {
            var exists = await _context.EjesObjetivosPN
                .AnyAsync(x => x.EjePnId == dto.EjePnId && x.ObjPnId == dto.ObjPnId && x.Estado == "A");

            if (exists)
                return "Esta relación ya existe.";

            var entity = new EjeObjetivoPN
            {
                EjePnId = dto.EjePnId,
                ObjPnId = dto.ObjPnId,
                CreadoPor = dto.CreadoPor
            };

            _context.EjesObjetivosPN.Add(entity);
            await _context.SaveChangesAsync();

            return "Relación creada correctamente.";
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.EjesObjetivosPN.FindAsync(id);
            if (entity == null || entity.Estado != "A")
                return false;

            entity.Estado = "I";
            entity.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<EjeObjetivoResponseDto>> GetByEjeIdAsync(int ejeId)
        {
            return await _context.EjesObjetivosPN
                .Where(x => x.EjePnId == ejeId && x.Estado == "A")
                .Include(x => x.Eje)
                .Include(x => x.Objetivo)
                .Select(x => new EjeObjetivoResponseDto
                {
                    EjeObjetivoPnId = x.EjeObjetivoPnId,

                    EjePnId = x.EjePnId,
                    EjeNombre = x.Eje.Nombre,

                    ObjPnId = x.ObjPnId,
                    ObjetivoNombre = x.Objetivo.Nombre,

                    Estado = x.Estado,
                    FechaCreacion = x.FechaCreacion
                })
                .ToListAsync();
        }

        public async Task<EjeJerarquicoResponseDto?> GetEjeJerarquicoAsync(int ejeId)
        {
            var eje = await _context.EjesPlanNacionalDesarrollo
                .Where(e => e.EjePnId == ejeId && e.Estado == "A")
                .Select(e => new EjeJerarquicoResponseDto
                {
                    EjePnId = e.EjePnId,
                    Nombre = e.Nombre,

                    Objetivos = _context.EjesObjetivosPN
                        .Where(eo => eo.EjePnId == e.EjePnId && eo.Estado == "A")
                        .Select(eo => eo.Objetivo)
                        .Where(o => o.Estado == "A")
                        .Select(o => new ObjetivoJerarquicoDto
                        {
                            ObjPnId = o.ObjPnId,
                            Nombre = o.Nombre,

                            Politicas = _context.ObjetivosPoliticasPN
                                .Where(op => op.ObjPnId == o.ObjPnId && op.Estado == "A")
                                .Select(op => op.Politica)
                                .Where(p => p.Estado == "A")
                                .Select(p => new PoliticaDto
                                {
                                    PoliticaPnId = p.PoliticaPnId,
                                    Nombre = p.Nombre
                                }).ToList(),

                            Metas = _context.ObjetivosMetasPN
                                .Where(om => om.ObjPnId == o.ObjPnId && om.Estado == "A")
                                .Select(om => om.Meta)
                                .Where(m => m.Estado == "A")
                                .Select(m => new MetaDto
                                {
                                    MetaPnId = m.MetaPnId,
                                    Nombre = m.Nombre
                                }).ToList()
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            return eje;
        }

    }
}