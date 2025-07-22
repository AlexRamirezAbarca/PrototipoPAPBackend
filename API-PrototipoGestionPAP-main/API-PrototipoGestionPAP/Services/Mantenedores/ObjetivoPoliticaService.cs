using API_PrototipoGestionPAP.Application.DTOs;
using API_PrototipoGestionPAP.Application.DTOs.Inbound;
using API_PrototipoGestionPAP.Application.DTOs.Outbound;
using API_PrototipoGestionPAP.Interfaces;
using API_PrototipoGestionPAP.Models;
using API_PrototipoGestionPAP.Models.Mantenedores;
using Microsoft.EntityFrameworkCore;

namespace API_PrototipoGestionPAP.Services.Mantenedores
{
    public class ObjetivoPoliticaService : IObjetivoPoliticaService
    {
        private readonly DBContext _context;

        public ObjetivoPoliticaService(DBContext context)
        {
            _context = context;
        }

        public async Task<ObjetivoPoliticaResponse?> GetByIdAsync(int id)
        {
            var relacion = await _context.ObjetivosPoliticasPN.FindAsync(id);
            if (relacion == null || relacion.estado == "N")
                return null;

            return new ObjetivoPoliticaResponse
            {
                ObjetivoPoliticaPnId = relacion.objetivo_politica_pn_id,
                ObjPnId = relacion.obj_pn_id,
                PoliticaPnId = relacion.politica_pn_id,
                Estado = relacion.estado,
                FechaCreacion = relacion.fecha_creacion
            };
        }

        public async Task<PaginatedResponse<ObjetivoPoliticaResponse>> GetAllPaginatedAsync(int page, int pageSize)
        {
            var query = _context.ObjetivosPoliticasPN
                .Where(x => x.estado == "A")
                .AsNoTracking();

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var data = await query
                .OrderByDescending(x => x.fecha_creacion)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ObjetivoPoliticaResponse
                {
                    ObjetivoPoliticaPnId = x.objetivo_politica_pn_id,
                    ObjPnId = x.obj_pn_id,
                    PoliticaPnId = x.politica_pn_id,
                    Estado = x.estado,
                    FechaCreacion = x.fecha_creacion
                })
                .ToListAsync();

            return new PaginatedResponse<ObjetivoPoliticaResponse>
            {
                Data = data,
                CurrentPage = page,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };
        }

        public async Task<ObjetivoPoliticaResponse> CreateAsync(CreateObjetivoPoliticaRequest request)
        {
            var relacion = new ObjetivoPoliticaPN
            {
                obj_pn_id = request.ObjPnId,
                politica_pn_id = request.ObjPnId,
                estado = "A",
                fecha_creacion = DateTime.UtcNow
            };

            _context.ObjetivosPoliticasPN.Add(relacion);
            await _context.SaveChangesAsync();

            return new ObjetivoPoliticaResponse
            {
                ObjetivoPoliticaPnId = relacion.objetivo_politica_pn_id,
                ObjPnId = relacion.obj_pn_id,
                PoliticaPnId = relacion.politica_pn_id,
                Estado = relacion.estado,
                FechaCreacion = relacion.fecha_creacion
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateObjetivoPoliticaRequest request)
        {
            var relacion = await _context.ObjetivosPoliticasPN.FindAsync(id);
            if (relacion == null || relacion.estado == "N")
                return false;

            relacion.obj_pn_id = request.ObjPnId;
            relacion.politica_pn_id = request.PoliticaPnId;
            relacion.fecha_modificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var relacion = await _context.ObjetivosPoliticasPN.FindAsync(id);
            if (relacion == null)
                return false;

            _context.ObjetivosPoliticasPN.Remove(relacion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
